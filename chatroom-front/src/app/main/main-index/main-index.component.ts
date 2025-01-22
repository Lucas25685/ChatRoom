import { Component, inject, OnInit } from '@angular/core';
import { MessagingService } from '../../_common/services/messaging/messaging.service';
import { ChatRoom } from 'src/app/_common/models/chat-room.model';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ChatMessage } from 'src/app/_common/models/chat-message.model';
import { AccountService } from 'src/app/_common/services/account/account.service';
import { RouterModule } from '@angular/router';

@Component({
	selector: 'app-main-index',
	standalone: true,
	imports: [CommonModule, RouterModule],
	styleUrl: './main-index.component.scss',
	templateUrl: './main-index.component.html',
})
export class MainIndexComponent implements OnInit {

	private subscription: Subscription = new Subscription();

	private readonly _messagingService = inject(MessagingService);
	private readonly _accountService = inject(AccountService);

	chatRooms: ChatRoom[] = [];
	messages: ChatMessage[] = [];

	async ngOnInit(): Promise<void> {
    this.subscription.add(
      (await (this._messagingService.getAllChatRooms())).subscribe({
        next: (rooms) => {
					this.chatRooms = rooms;
				},
        error: (err) => console.error('Error:', err)
      })
    );
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

	public createChatRoom() {
		this._messagingService.createChatRoom();
		this.ngOnInit();
	}

	public async joinChatRoom(roomId: string) {
		const userId = this._accountService.user()?.id;
		if (userId) {
		  this._messagingService.joinChatRoom(roomId, userId).then(messages => {
			this.messages = messages;
		  }).catch(error => {
			console.error('Failed to join room or fetch messages:', error);
		  });
		} else {
		  console.error('User is not authenticated');
		}
	  }
}

import { Component, inject, OnInit, Signal } from '@angular/core';
import { ChatSvgLogoComponent } from '../../_common/components/chat-svg-logo/chat-svg-logo.component';
import { ChatButtonComponent } from '../../_common/components/chat-button/chat-button.component';
import { MessagingService } from '../../_common/services/messaging/messaging.service';
import { ChatRoom } from 'src/app/_common/models/chat-room.model';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ChatMessage } from 'src/app/_common/models/chat-message.model';

@Component({
	selector: 'app-main-index',
	standalone: true,
	imports: [CommonModule],
	styleUrl: './main-index.component.scss',
	templateUrl: './main-index.component.html',
})
export class MainIndexComponent implements OnInit {

	private subscription: Subscription = new Subscription();

	private readonly _messagingService = inject(MessagingService);

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
    this._messagingService.joinChatRoom(roomId).then(messages => {
      this.messages = messages;
    }).catch(error => {
      console.error('Failed to join room or fetch messages:', error);
    });
	}
}

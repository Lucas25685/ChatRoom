import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessagingService } from '../../_common/services/messaging/messaging.service';
import { AccountService } from '../../_common/services/account/account.service';
import { ChatMessage } from '../../_common/models/chat-message.model';

@Component({
  selector: 'app-main-chat-room',
  templateUrl: './main-chat-room.component.html',
  styleUrls: ['./main-chat-room.component.scss']
})
export class MainChatRoomComponent implements OnInit {
  messages: ChatMessage[] = [];
  roomId: string | undefined;

  constructor(
    private route: ActivatedRoute,
    @Inject(MessagingService) private messagingService: MessagingService,
    @Inject(AccountService) private accountService: AccountService
  ) {}

  async ngOnInit(): Promise<void> {
    this.roomId = this.route.snapshot.paramMap.get('id')!;
    const userId = this.accountService.user()?.id;
    if (userId) {
      this.messages = await this.messagingService.joinChatRoom(this.roomId, userId);
    } else {
      console.error('User is not authenticated');
    }
  }
}
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ChatMessage } from '../../models/chat-message.model';
import { ChatRoom } from '../../models/chat-room.model';
import { SignalRClientBase } from '../signalr/signalr.client.base';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class MessagingService extends SignalRClientBase {

	constructor() {
		super(environment.API_URL + '/hub/messaging');

		// Handle messaging events
		this._hubConnection.on('NewMessage', (message: ChatMessage) => {
			console.log('New message received:', message);

		});

		this._hubConnection.on('EditedMessage', (message: ChatMessage) => {
		});

		this._hubConnection.on('DeletedMessage', (message: ChatMessage) => {
		});

		this._hubConnection.on('UserWriting', (user: ChatMessage) => {
		});
	}

	/**
	 * Get a chat room for the offer provided
	 * @param roomId
	 * @returns chatroom
	 */
	public async getChatRoom(roomId: string): Promise<ChatRoom> {
		await this.getConnectionPromise;

		return await this._hubConnection.invoke<ChatRoom>('GetChatRoom', roomId);
	}

	/**
	 * Create a new chat room
	 */
	public async createChatRoom(): Promise<ChatRoom> {
		await this.getConnectionPromise;

		return await this._hubConnection.invoke<ChatRoom>('CreateChatRoom');
	}

	public async joinChatRoom(roomId: string): Promise<ChatMessage[]> {
    await this.getConnectionPromise;

    // Invoke the method on the SignalR server and receive the chat history
    const chatHistory: ChatMessage[] = await this._hubConnection.invoke<ChatMessage[]>('JoinChatRoom', roomId);

    return chatHistory;
}

	/**
	 * Get all chat room messages
	 */
	public async leaveChatRoom(roomId: string): Promise<void> {
		await this.getConnectionPromise;

		await this._hubConnection.invoke('LeaveChatRoom', roomId);
	}

	/**
	 * Send message to the chat room
	 */
	public async sendMessage(roomId: string, message: string): Promise<any> {
		await this.getConnectionPromise;

		await this._hubConnection.invoke('SendMessage', roomId, message);
	}

		/**
	 * Send message to the chat room
	 */
		public async getAllChatRooms(): Promise<Observable<ChatRoom[]>> {
			return new Observable<ChatRoom[]>((observer) => {
				this.getConnectionPromise.then(() => {
					this._hubConnection.invoke<ChatRoom[]>('GetAllChatRooms')
						.then((chatRooms) => {
							observer.next(chatRooms);
							observer.complete();
						})
						.catch((err) => observer.error(err));
				});
			});
		}
}

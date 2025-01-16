import { Company } from './company.model';
import { ChatMessage } from './chat-message.model';

export interface ChatRoom {
	id: string;
	participants: Company[];
	messages: ChatMessage[];
	createdAt: Date;
	updatedAt: Date;
	readOnly: boolean;
}

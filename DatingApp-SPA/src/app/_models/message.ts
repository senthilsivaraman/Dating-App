export interface Message {
    id: number;
    senderId: number;
    sender: string;
    senderPhotoUrl: string;
    recipientId: number;
    recipient: string;
    recipientPhotoUrl: string;
    content: string;
    isRead: string;
    messageReadTime: Date;
    messageSentTime: Date;

}

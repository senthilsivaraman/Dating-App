import { Photo } from './photo';

export interface User {
    id: number;
    name: string;
    fullName: string;
    gender: string;
    age: number;
    accCreated: Date;
    lastSeen: any;
    photoUrl: string;
    currentCity: string;
    raisedCity: string;
    relationshipStatus: string;
    lookingFor: string;
    email?: string;
    bio?: string;
    religion?: string;
    interestedIn?: string;
    photos?: Photo[];
}

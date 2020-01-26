import { Photo } from './photo';

export interface User {
    id: number;
    name: string;
    fullName: string;
    gender: string;
    age: number;
    accCreated: Date;
    lastSeen: Date;
    currentCity: string;
    raisedCity: string;
    relationshipStatus: string;
    lookingFor: string;
    email?: string;
    bio?: string;
    religion?: string;
    interstedIn?: string;
    photos?: Photo[];
}

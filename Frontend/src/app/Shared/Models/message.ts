export interface Message {
  userId: string;
  username: string;
  message: string;
  timestamp: string;
  
  sentiment: number;
  positiveScore: number;
  negativeScore: number;
  neutralScore: number;
}

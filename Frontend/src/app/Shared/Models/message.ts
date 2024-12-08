export interface Message {
  UserId: string;
  Username: string;
  Message: string;
  Timestamp: Date;
  
  Sentiment: number;
  PositiveScore: number;
  NegativeScore: number;
  NeutralScore: number;
}

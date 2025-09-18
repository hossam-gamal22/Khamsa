namespace Project.General
{
    public static class GameConstants
    {
        // Coins
        public const int STARTING_COINS = 100;
        public const int DAILY_BONUS_COINS = 10;
        public const int AD_REWARD_COINS = 25;
        public const int ROOM_WIN_COINS = 5;
        
        // Daily Rewards
        public static readonly int[] DAILY_REWARDS = { 20, 50, 150, 200, 300, 500, 1500 };
        public const int BONUS_REWARD = 1500;
        
        // Ads
        public const int MAX_ADS_PER_DAY = 10;
        public const int AD_COOLDOWN_MINUTES = 10;
        
        // Room Creation
        public const int MIN_ROOM_CREATION_COST = 10;
        
        // Boosts
        public const int EXTRA_TIME_COST = 5;
        public const int SKIP_POINTS_COST = 10;
        public const int EXTRA_HINT_COST = 5;
        
        // 7arifa Challenge
        public const int HARIFA_FIRST_PRIZE = 50;
        public const int HARIFA_SECOND_PRIZE = 30;
        public const int HARIFA_THIRD_PRIZE = 20;
        
        // Username
        public const int MAX_USERNAME_LENGTH = 12;
        
        // Avatar Prices
        public const int MIN_AVATAR_PRICE = 100;
        public const int MAX_AVATAR_PRICE = 1000;
        
        // Sticker Prices
        public const int MIN_STICKER_PRICE = 300;
        public const int MAX_STICKER_PRICE = 800;
        
        // Sound Prices  
        public const int MIN_SOUND_PRICE = 200;
        public const int MAX_SOUND_PRICE = 2000;
    }
}
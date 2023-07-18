namespace GoodGameDatabase.Common
{
    public static class EntityValidationConstants
    {
        public static class Game
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 20;

            public const int DescriptionMinLength = 30;
            public const int DescriptionMaxLength = 200;
        }
    }
}
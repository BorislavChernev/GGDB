namespace GoodGameDatabase.Common
{
    public static class EntityValidationConstants
    {
        public static class Game
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 20;
            public const string NameErrorMessage = "Name length should be between 2 and 20 characters";

            public const int DescriptionMinLength = 30;
            public const int DescriptionMaxLength = 200;
            public const string DescriptionErrorMessage = "Description length should be between 30 and 200 characters";
        }
    }
}
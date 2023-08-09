namespace GoodGameDatabase.Common
{
    public static class EntityValidationConstants
    {
        public static class Game
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
            public const string NameErrorMessage = "Name length should be between 2 and 50 characters";

            public const int DescriptionMinLength = 30;
            public const int DescriptionMaxLength = 1000;
            public const string DescriptionErrorMessage = $"Description length should be between 30 and 1000 characters";
        }

        public static class Discussion
        {
            public const int TopicMinLength = 2;
            public const int TopicMaxLength = 50;
            public const string TopicErrorMessage = "Topic length should be between 2 and 50 characters";

            public const int DescriptionMinLength = 30;
            public const int DescriptionMaxLength = 1000;
            public const string DescriptionErrorMessage = $"Description length should be between 30 and 1000 characters";
        }

        public static class Guide
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 100;
            public const string TitleErrorMessage = "Title length should be between 2 and 100 characters";

            public const int DescriptionMinLength = 30;
            public const int DescriptionMaxLength = 1000;
            public const string DescriptionErrorMessage = $"Description length should be between 30 and 1000 characters";
        }

        public static class New
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 100;
            public const string TitleErrorMessage = "Title length should be between 2 and 100 characters";

            public const int SubtitleMinLength = 2;
            public const int SubtitleMaxLength = 100;
            public const string SubtitleErrorMessage = "Subtitle length should be between 2 and 100 characters";


            public const int DescriptionMinLength = 30;
            public const int DescriptionMaxLength = 1000;
            public const string DescriptionErrorMessage = $"Description length should be between 30 and 1000 characters";
        }
    }
}
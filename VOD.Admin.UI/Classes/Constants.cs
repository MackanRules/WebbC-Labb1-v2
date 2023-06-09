﻿namespace VOD.Admin.UI.Classes
{

    // Vilken entitet vi arbetar med
    public static class Routes
    {
        public static string Films => "Films";
        public static string Directors => "Directors";
        public static string Genres => "Genres";
        public static string FilmGenres => "FilmGenres";
        public static string SimilarFilms => "SimilarFilms";
    }

    // Vad vi vill göra med entiteten
    public static class PageType
    {
        public static string Index => "Index";
        public static string Create => "Create";
        public static string Edit => "Edit";
        public static string Delete => "Delete";
    }
}

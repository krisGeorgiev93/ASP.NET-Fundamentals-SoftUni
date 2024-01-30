﻿using System.ComponentModel.DataAnnotations;

namespace BookShoppingApp.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string GenreName { get; set; }

        public List<Book> Books { get; set; }

    }
}

﻿using System;

namespace BooksDatabase
{
    public class Publisher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}


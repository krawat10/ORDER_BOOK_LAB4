﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LAB4_150348.Models;

namespace LAB4_150348.Seeders
{
    public class ApplicationDbContextSeeder: IDbSeeder<ApplicationDbContext>
    {
        async Task<bool> IDbSeeder<ApplicationDbContext>.Seed(ApplicationDbContext context)
        {
            var books = new List<Book>
            {
                new Book {Author = "JK Rowling", Title = "Harry Potter", AvailableAmount = 10, Price = 100},
                new Book {Author = "Tokien", Title = "Lord of ring", AvailableAmount = 15, Price = 90},
                new Book {Author = "Frank D. Luna", Title = "3D Game Programing with DirectX 12", AvailableAmount = 5, Price = 110},
            };


            await context.AddRangeAsync(books);

            return await context.SaveChangesAsync() > 0;
        }

    }
}
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace BookStore.Services
{
    public class BookStoreItemService : IItemService
    {
        private readonly BookStoreDBContext _databaseContext;        

        public BookStoreItemService(BookStoreDBContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> DeleteItem(ViewModelBase viewModel)
        {
            BookListViewModel bookListViewModel = viewModel as BookListViewModel;

            if (bookListViewModel != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    bookListViewModel.Books.Remove(bookListViewModel.SelectedBook);
                    return await DeleteTransaction(bookListViewModel.SelectedBook.BookId) > 0;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SaveItem(ViewModelBase viewModel)
        {
            BookListViewModel bookListViewModel = viewModel as BookListViewModel;

            if (bookListViewModel != null)
            {
                if(bookListViewModel.SelectedBook?.BookId == 0)
                {
                    //new item add case
                    AddNewBook(bookListViewModel.SelectedBook);
                }
                else
                {
                    //modify exist item case
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<int> DeleteTransaction(int bookId)
        {
            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _databaseContext.Database.BeginTransaction(IsolationLevel.Snapshot))
            {
                try
                {
                    //delete from Books table
                    var selectedBook = _databaseContext.Books.Where<Book>(b => b.BookId == bookId).FirstOrDefault<Book>();
                    _databaseContext.Books.Remove(selectedBook);

                    //delete from BookAutors table
                    var deleteBookAuthorList = _databaseContext.BookAuthors.Where<BookAuthor>(ba => ba.BookId == bookId).ToList<BookAuthor>();
                    _databaseContext.RemoveRange(deleteBookAuthorList);

                    //delete from BookCategories table
                    var deleteBookCategoryList = _databaseContext.BookCategories.Where<BookCategory>(bc => bc.BookId == bookId).ToList<BookCategory>();
                    _databaseContext.RemoveRange(deleteBookCategoryList);

                    //delete from BookFormats table
                    var deleteBookFormatsList = _databaseContext.BookFormats.Where<BookFormat>(bf => bf.BookId == bookId).ToList<BookFormat>();
                    _databaseContext.RemoveRange(deleteBookFormatsList);

                    var effectedRows = await _databaseContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return effectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Delete transaction was NOT succesfull! The reason: " + ex.Message);
                    return -1;
                }
                
            }
        }

        private async Task<int> AddNewBook(BookPresenterModel SelectedBook)
        {
            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _databaseContext.Database.BeginTransaction())
            {

                Book book = new Book();

                try
                {
                    //First check Authors
                    foreach (var author in SelectedBook.BookAuthors)
                    {
                        var foundAuthor = _databaseContext.Authors.Where<Author>(a => (a.AuthorFirstName == author.AuthorFirstName) && (a.AuthorLastName == author.AuthorLastName)).FirstOrDefault<Author>();
                        if (foundAuthor != null)
                        {
                            book.BookAuthors.Add(new BookAuthor()
                            {
                                Author = foundAuthor
                            });
                        }
                        else
                        {
                            _databaseContext.Authors.Add(author);
                            book.BookAuthors.Add(new BookAuthor()
                            {
                                Author = author
                            });
                        }
                    }

                    //Category check
                    foreach (var category in SelectedBook.BookCategories)
                    {
                        var foundCategory = _databaseContext.Categories.Where<Category>(c => c.CategoryName.Equals(category.CategoryName)).FirstOrDefault<Category>();
                        if (foundCategory != null)
                        {
                            book.BookCategories.Add(new BookCategory()
                            {
                                Category = foundCategory
                            });
                        }
                        else
                        {
                            _databaseContext.Categories.Add(category);
                            book.BookCategories.Add(new BookCategory()
                            {
                                Category = category
                            });
                        }
                    }

                    //Format check
                    foreach (var format in SelectedBook.BookFormats)
                    {
                        var foundFormat = _databaseContext.Formats.Where<Format>(f => f.FormatName.Equals(format.FormatName)).FirstOrDefault<Format>();
                        if (foundFormat != null)
                        {
                            book.BookFormats.Add(new BookFormat()
                            {
                                Format = foundFormat
                            });
                        }
                        else
                        {
                            _databaseContext.Formats.Add(format);
                            book.BookFormats.Add(new BookFormat()
                            {
                                Format = format
                            });
                        }
                    }


                    book.BookName = SelectedBook.BookName;
                    book.Description = SelectedBook.Description;
                    book.PictureUrl = SelectedBook.PictureUrl;
                    book.Price = SelectedBook.Price;

                    var publisher = _databaseContext.Publishers.Where<Publisher>(p => p.PublisherName.Equals(SelectedBook.Publisher));
                    if (publisher != null)
                    {
                        book.Publisher = publisher.FirstOrDefault<Publisher>();
                    }
                    else
                    {
                        Publisher p = new Publisher()
                        {
                            PublisherName = SelectedBook.Publisher
                        };
                        book.Publisher = p;
                    }

                    await _databaseContext.Books.AddAsync(book);

                    var effectedRows = await _databaseContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return effectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Insert transaction was not succesfull! The reason: " + ex.Message);
                    return 0;
                }

            }
        }
    }
}

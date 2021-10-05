using BookStore.Commands;
using BookStore.Models;
using BookStore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BookStore.ViewModels
{
    public class BookListViewModel : ViewModelBase
    {
        private readonly BookStoreDBContext _databaseContext;
        private readonly IItemService _bookStoreItemService;
        private BookPresenterModel _selectedBook;
        private ObservableCollection<BookPresenterModel> _books;

        //public MyICommand DeleteCommand { get; set; }
        public AsyncCommandBase DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public AsyncCommandBase SaveCommand { get; set; }

        public BookListViewModel(BookStoreDBContext databaseContext, IItemService bookStoreItemService)
        {
            _databaseContext = databaseContext;
            _bookStoreItemService = bookStoreItemService;

            //fill up at startup
            _books = FillBookList();

            //DeleteCommand = new MyICommand(OnDelete, CanDelete);
            DeleteCommand = new DeleteCommand(this, _bookStoreItemService);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            SaveCommand = new SaveCommand(this, _bookStoreItemService);
        }

        public BookPresenterModel SelectedBook
        {
            get
            {
                return _selectedBook;
            }

            set
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<BookPresenterModel> Books
        {
            get
            {
                return _books;
            }
            set
            {
                _books = value;
                OnPropertyChanged(nameof(Books));
            }
        }

        private void OnAdd()
        {
            Books.Add(new BookPresenterModel());
            //AddNewBook();
        }

        private bool CanAdd()
        {
            return true;
        }        

        private ObservableCollection<BookPresenterModel> FillBookList()
        {

            List<Author> authors = _databaseContext.Authors.ToList();
            List<Book> books = _databaseContext.Books.ToList();
            List<BookAuthor> bookAuthors = _databaseContext.BookAuthors.ToList();
            List<BookCategory> bookCategories = _databaseContext.BookCategories.ToList();
            List<BookFormat> bookFormats = _databaseContext.BookFormats.ToList();
            List<Category> categories = _databaseContext.Categories.ToList();
            List<Format> formats = _databaseContext.Formats.ToList();
            List<Publisher> publishers = _databaseContext.Publishers.ToList();

            var queryResult = from book in books
                              select new BookPresenterModel
                              {
                                  BookId = book.BookId,
                                  BookName = book.BookName,
                                  BookAuthors = new ObservableCollection<Author>((from author in authors
                                                                                  join bookauthor in bookAuthors on book.BookId equals bookauthor.BookId
                                                                                  where bookauthor.AuthorId == author.AuthorId
                                                                                  select new Author
                                                                                  {
                                                                                      AuthorId = author.AuthorId,
                                                                                      AuthorFirstName = author.AuthorFirstName,
                                                                                      AuthorLastName = author.AuthorLastName,
                                                                                      Description = author.Description

                                                                                  }).ToList<Author>()),
                                  Publisher = (from publisher in publishers
                                               where book.PublisherId == publisher.PublisherId
                                               select new Publisher
                                               {
                                                   PublisherId = publisher.PublisherId,
                                                   PublisherName = publisher.PublisherName
                                               }).First().PublisherName,
                                  PictureUrl = book.PictureUrl,
                                  Description = book.Description,
                                  Price = book.Price,
                                  BookCategories = new ObservableCollection<Category>((from category in categories
                                                                                       join bookcategory in bookCategories on category.CategoryId equals bookcategory.CategoryId
                                                                                       where bookcategory.BookId == book.BookId
                                                                                       select new Category
                                                                                       {
                                                                                           CategoryId = category.CategoryId,
                                                                                           CategoryName = category.CategoryName
                                                                                       }).ToList<Category>()),
                                  BookFormats = new ObservableCollection<Format>((from format in formats
                                                                                  join bookformat in bookFormats on format.FormatId equals bookformat.FormatId
                                                                                  where bookformat.BookId == book.BookId
                                                                                  select new Format
                                                                                  {
                                                                                      FormatId = format.FormatId,
                                                                                      FormatName = format.FormatName,
                                                                                      Description = format.Description
                                                                                  }).ToList<Format>())
                              };
            return new ObservableCollection<BookPresenterModel>(queryResult.ToList());
        }

        private void DeleteTransaction(int bookId)
        {
            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _databaseContext.Database.BeginTransaction())
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

                    _databaseContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Delete transaction was NOT succesfull! The reason: " + ex.Message);
                }
            }
        }

        private void AddNewBook()
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
                        book.Publisher = new Publisher()
                        {
                            PublisherName = SelectedBook.Publisher
                        };
                    }

                    _databaseContext.Books.Add(book);

                    _databaseContext.SaveChanges();
                    transaction.Commit();

                    Books.Add(new BookPresenterModel()
                    {
                        BookId = book.BookId,
                        BookName = book.BookName,
                        Description = book.Description,
                        PictureUrl = book.PictureUrl,
                        Price = book.Price,
                        Publisher = book.Publisher.PublisherName,
                        BookAuthors = SelectedBook.BookAuthors,
                        BookCategories = SelectedBook.BookCategories,
                        BookFormats = SelectedBook.BookFormats
                    });

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Insert transaction was not succesfull! The reason: " + ex.Message);

                }

            }
        }
    }
}
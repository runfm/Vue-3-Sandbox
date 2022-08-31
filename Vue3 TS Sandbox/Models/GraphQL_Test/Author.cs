using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vue3_TS_Sandbox.Models.GraphQL_Test {
    public class Author {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class BlogPost {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
    }

    public class AuthorType : ObjectGraphType<Author> {
        public AuthorType() {
            Name = "Author";
            Field(_ => _.Id).Description("Author's Id.");
            Field(_ => _.FirstName).Description
            ("First name of the author");
            Field(_ => _.LastName).Description
            ("Last name of the author");
        }
    }

    public class BlogPostType : ObjectGraphType<BlogPost> {
        public BlogPostType() {
            Name = "BlogPost";
            Field(_ => _.Id, type:
            typeof(IdGraphType)).Description
           ("The Id of the Blog post.");
            Field(_ => _.Title).Description
            ("The title of the blog post.");
            Field(_ => _.Content).Description
            ("The content of the blog post.");
        }
    }

    public class AuthorQuery : ObjectGraphType {
        public AuthorQuery(AuthorService authorService) {
            int id = 0;
            Field<ListGraphType<AuthorType>>(
            name: "authors", resolve: context => {
                return authorService.GetAllAuthors();
            });
            Field<AuthorType>(
                name: "author",
                arguments: new QueryArguments(new
                QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => {
                    id = context.GetArgument<int>("id");
                    return authorService.GetAuthorById(id);
                }
            );
            Field<ListGraphType<BlogPostType>>(
                name: "blogs",
                arguments: new QueryArguments(new
                QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => {
                    return authorService.GetPostsByAuthor(id);
                }
            );
        }
    }

    public class GraphQLQueryDTO {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public object Variables { get; set; }
    }

    public class AuthorService {
        private readonly AuthorRepository _authorRepository;

        public AuthorService(AuthorRepository
                authorRepository) {
            _authorRepository = authorRepository;
        }
        public List<Author> GetAllAuthors() {
            return _authorRepository.GetAllAuthors();
        }
        public Author GetAuthorById(int id) {
            return _authorRepository.GetAuthorById(id);
        }
        public List<BlogPost> GetPostsByAuthor(int id) {
            return _authorRepository.GetPostsByAuthor(id);
        }
    }


    public class AuthorRepository {
        private readonly List<Author> authors =
        new List<Author>();
        private readonly List<BlogPost> posts =
        new List<BlogPost>();

        public AuthorRepository() {
            Author author1 = new Author {
                Id = 1,
                FirstName = "Joydip",
                LastName = "Kanjilal"
            };
            Author author2 = new Author {
                Id = 2,
                FirstName = "Steve",
                LastName = "Smith"
            };
            BlogPost csharp = new BlogPost {
                Id = 1,
                Title = "Mastering C#",
                Content = "This is a series of articles on C#.",
                Author = author1
            };
            BlogPost java = new BlogPost {
                Id = 2,
                Title = "Mastering Java",
                Content = "This is a series of articles on Java",
                Author = author1
            };
            posts.Add(csharp);
            posts.Add(java);
            authors.Add(author1);
            authors.Add(author2);
        }
        public List<Author> GetAllAuthors() {
            return this.authors;
        }
        public Author GetAuthorById(int id) {
            return authors.Where(author => author.Id ==
            id).FirstOrDefault<Author>();
        }
        public List<BlogPost> GetPostsByAuthor(int id) {
            return posts.Where(post => post.Author.Id ==
            id).ToList<BlogPost>();
        }
    }
}

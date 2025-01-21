using PNE07;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
try
{
    using (InstagramContext context = new InstagramContext())
    {
        
        if (!context.Users.Any())
        {
            SeedTestData(context);
            Console.WriteLine("Testovací data byla úspěšně přidána.");
        }

        // UKOL vypisování
        //01
        Console.WriteLine();
        FindOldPostsWithoutMe(context, "Pavel", "Modrý");
        //02
        Console.WriteLine();
        FindAllLikedPostsByOneUser(context, "Jirka", "Žlutý");
        //03
        Console.WriteLine();
        PostLikedByTwoUsers(context);
        //04 - zadat uživatele, který je ten co má sledovat
        Console.WriteLine();
        PostMoreConditions(context, "Jindřich", "Vomáčka");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

static void FindOldPostsWithoutMe(InstagramContext context, string firstName, string lastName)
{
    var posts = context.Posts
        .Where(p => p.User.FirstName == firstName && p.User.LastName == lastName) 
        .Where(p => p.ReleaseDate < DateTime.Now.AddDays(-7))
        .Where(p => !p.Text.ToLower().Contains("já"))
        .ToList();

    if (posts.Any())
    {
        Console.WriteLine($"Všechny příspěvky uživatele {firstName} {lastName} starší než týden bez já:");
        foreach (var post in posts)
        {
            Console.WriteLine($"-----Text příspěvku: {post.Text}");
        }
    }
    else
    {
        Console.WriteLine($"Uživatel {firstName} {lastName} nemá žádný příspěvek starší než týden bez já.");
    }
}

static void FindAllLikedPostsByOneUser(InstagramContext context, string firstName, string lastName)
{
    var posts = context.Posts
        .Where(p => p.Likes.Any(u => u.FirstName == firstName && u.LastName == lastName))
        .ToList();

    if (posts.Any())
    {
        Console.WriteLine($"Všechny příspěvky, které {firstName} {lastName} ĺajknul:");
        foreach (var post in posts)
        {
            Console.WriteLine($"    Text příspěvku: {post.Text}");
        }
    }
    else
    {
        Console.WriteLine($"Uživatel {firstName} {lastName} nelajknul žádný příspěvek.");
    }
}

static void PostLikedByTwoUsers(InstagramContext context)
{
    var posts = context.Posts
        .Where(p => p.Likes
            .Select(u => u.Id)
            .Distinct() 
            .Count() >= 2)
        .ToList();

    if (posts.Any())
    {
        Console.WriteLine($"Všechny příspěvky, které lajknuli dva různí uživatelé");
        foreach (var post in posts)
        {
            Console.WriteLine($"    Text příspěvku: {post.Text}");
        }
    }
    else
    {
        Console.WriteLine($"Žádný příspěvek nemá dva a víc lajků");
    }
}


static void PostMoreConditions(InstagramContext context, string firstName, string lastName)
{
    // nalezení konkrétního uživatele, který sleduje ostatní
    var user = context.Users
        .Where(u => u.FirstName == firstName && u.LastName == lastName)
        .FirstOrDefault();

    if (user == null)
    {
        Console.WriteLine("Uživatel neexistuje.");
        return;
    }

    var posts = context.Posts
        .Where(p => p.User.Followed.Contains(user))
        .Where(p => p.ReleaseDate >= DateTime.Now.AddDays(-3))
        .OrderByDescending(p => p.ReleaseDate)
        .ToList();

    if (posts.Any())
    {
        Console.WriteLine($"Příspěvky všech uživatelů, které sleduje {firstName} {lastName} a příspěvky jsou staré max 3 dny (seřazeno podle data):");
        foreach (var post in posts)
        {
            Console.WriteLine($"    Text příspěvku: {post.Text}, {post.ReleaseDate}");
        }
    }
    else
    {
        Console.WriteLine("Neexistují žádné příspěvky splňující podmínky.");
    }
}

static void SeedTestData(InstagramContext context) // data generovane chat gpt
{
    User usr1 = new User { FirstName = "Pavel", LastName = "Modrý", Age = 78 };
    User usr2 = new User { FirstName = "Jirka", LastName = "Žlutý", Age = 33 };
    User usr3 = new User { FirstName = "Ondřej", LastName = "Fialový", Age = 13 };
    User usr4 = new User { FirstName = "Aneta", LastName = "Béžová", Age = 48 };
    User usr5 = new User { FirstName = "Michal", LastName = "Hnědý", Age = 20 };
    User usr6 = new User { FirstName = "Jindřich", LastName = "Vomáčka", Age = 13 };
    User usr7 = new User { FirstName = "Kevin", LastName = "Bagr", Age = 91 };

    context.Users.AddRange(usr1, usr2, usr3, usr4, usr5, usr6, usr7);

    Post post1 = new Post { Text = "Dneska jsem potkal na ulici kočku, co měla klobouk!", ReleaseDate = DateTime.Now.AddDays(-10), User = usr1, ImgPath = "images/post1.jpg" };
    Post post2 = new Post { Text = "Myslím, že bych měl začít běhat, ale zítra...", ReleaseDate = DateTime.Now.AddDays(-8), User = usr2, ImgPath = "uploads/images/IMG002.jpg" };
    Post post3 = new Post { Text = "Přemýšlím o tom, že si koupím novou kytaru.", ReleaseDate = DateTime.Now.AddDays(-6), User = usr3, ImgPath = "media/posts/3.png" };
    Post post4 = new Post { Text = "Dneska jsem měl šílenou ranní kávu, měla tolik cukru, že mi bylo špatně.", ReleaseDate = DateTime.Now.AddDays(-5), User = usr4, ImgPath = "pictures/coffeePost.jpg" };
    Post post5 = new Post { Text = "Večer chci zkusit nový recept na špagety.", ReleaseDate = DateTime.Now.AddDays(-4), User = usr5, ImgPath = "posts/images/spaghetti_recept.png" };
    Post post6 = new Post { Text = "Tento víkend jedu na hory, konečně si odpočinu.", ReleaseDate = DateTime.Now.AddDays(-3), User = usr1, ImgPath = "images/mountains_vacation.jpg" };
    Post post7 = new Post { Text = "Nechápu, proč mi všichni říkají, že mám spát víc... zaspal jsem a byl jsem v pohodě!", ReleaseDate = DateTime.Now.AddDays(-2), User = usr2, ImgPath = "photos/sleepyPost.jpg" };
    Post post8 = new Post { Text = "Nemůžu si vzpomenout, kde jsem nechal klíče od auta.", ReleaseDate = DateTime.Now.AddDays(-1), User = usr3, ImgPath = "uploads/lost_keys.jpg" };
    Post post9 = new Post { Text = "Dneska jsem měl zkoušku a doufám, že to dopadlo dobře. Přeji si to.", ReleaseDate = DateTime.Now.AddDays(-1), User = usr4, ImgPath = "media/exam_day.png" };
    Post post10 = new Post { Text = "Snídaně je moje nejoblíbenější jídlo dne!", ReleaseDate = DateTime.Now, User = usr5, ImgPath = "pictures/breakfast_time.jpg" };

    context.Posts.AddRange(post1, post2, post3, post4, post5, post6, post7, post8, post9, post10);

    Comment comment1 = new Comment { Text = "No to snad ne, kdo si koupí tuhle kytaru?", User = usr2, Post = post1 };
    Comment comment2 = new Comment { Text = "To je fakt šílený, proč jsi to neřekl dřív?", User = usr3, Post = post2 };
    Comment comment3 = new Comment { Text = "Přesně, taky to tak mám, škoda že nemám víc času na spánek.", User = usr4, Post = post5 };

    context.Comments.AddRange(comment1, comment2, comment3);

    Message message1 = new Message { Text = "Ahoj, co děláš dneska večer?", Sender = usr1, Receiver = usr2 };
    Message message2 = new Message { Text = "Mám novou práci, začínám za týden!", Sender = usr2, Receiver = usr3 };
    Message message3 = new Message { Text = "Něco ti musím říct, mám skvělou novinku!", Sender = usr3, Receiver = usr4 };
    Message message4 = new Message { Text = "Kdybych měl víc času, asi bych začal programovat, co myslíš?", Sender = usr4, Receiver = usr5 };
    Message message5 = new Message { Text = "Taky chci začít běhat, kde chodíš na tréninky?", Sender = usr5, Receiver = usr1 };
    Message message6 = new Message { Text = "Hele, víš co, pojďme na kafe ve středu!", Sender = usr1, Receiver = usr4 };
    Message message7 = new Message { Text = "Kdybych mohl, hned bych šel, ale mám dneska plno", Sender = usr2, Receiver = usr3 };
    Message message8 = new Message { Text = "Kdybych měl víc peněz, hned bych si koupil nový kolo!", Sender = usr3, Receiver = usr5 };
    Message message9 = new Message { Text = "Viděl jsi ten film, co doporučil Honza?", Sender = usr4, Receiver = usr2 };
    Message message10 = new Message { Text = "Mám toho teď hodně, ale brzy se ti ozvu.", Sender = usr5, Receiver = usr1 };

    context.Messages.AddRange(message1, message2, message3, message4, message5, message6, message7, message8, message9, message10);

    post1.Likes.Add(usr2);
    post2.Likes.Add(usr1);
    post3.Likes.Add(usr1);
    post4.Likes.Add(usr1);
    post5.Likes.Add(usr1);
    post6.Likes.Add(usr2);
    post7.Likes.Add(usr6);
    post8.Likes.Add(usr7);
    post1.Likes.Add(usr7);
    post2.Likes.Add(usr7);
    post3.Likes.Add(usr7);

    usr1.Following.Add(usr2);
    usr2.Following.Add(usr3);
    usr3.Following.Add(usr4);
    usr4.Following.Add(usr5);
    usr5.Following.Add(usr6);
    usr6.Following.Add(usr7);
    usr7.Following.Add(usr1);
    usr6.Following.Add(usr2);
    usr6.Following.Add(usr3);
    usr6.Following.Add(usr4);

    context.SaveChanges();
}
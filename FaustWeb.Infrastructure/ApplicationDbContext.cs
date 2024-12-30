using FaustWeb.Domain.Entities;
using FaustWeb.Domain.Entities.Comments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FaustWeb.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<AssignedTag> AssignedTags { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<ChapterComment> ChapterComments { get; set; }
    public DbSet<ChapterDislike> ChapterDislikes { get; set; }
    public DbSet<ChapterLike> ChapterLikes { get; set; }
    public DbSet<CommentDislike<ChapterComment>> ChapterCommentDislikes { get; set; }
    public DbSet<CommentLike<ChapterComment>> ChapterCommentLikes { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<TitleComment> TitleComments { get; set; }
    public DbSet<CommentDislike<TitleComment>> TitleCommentDislikes { get; set; }
    public DbSet<CommentLike<TitleComment>> TitleCommentLikes { get; set; }
    public DbSet<TitleDislike> TitleDislikes { get; set; }
    public DbSet<TitleLike> TitleLikes { get; set; }
    public DbSet<TitleSaved> SavedTitles { get; set; }
    public DbSet<TranslationTeam> TranslationTeams { get; set; }
    public DbSet<TranslationTeamMember> TranslationTeamMembers { get; set; }
    public DbSet<Volume> Volumes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssignedTag>()
            .HasKey(assignedTag => new { assignedTag.TitleId, assignedTag.TagId });

        modelBuilder.Entity<Chapter>()
            .HasMany(chapter => chapter.Comments)
            .WithOne(comment => comment.Chapter)
            .HasForeignKey(comment => comment.ChapterId);

        modelBuilder.Entity<Chapter>()
            .HasMany(chapter => chapter.Dislikes)
            .WithOne(dislike => dislike.Chapter)
            .HasForeignKey(dislike => dislike.ChapterId);

        modelBuilder.Entity<Chapter>()
            .HasMany(chapter => chapter.Likes)
            .WithOne(dislike => dislike.Chapter)
            .HasForeignKey(dislike => dislike.ChapterId);

        modelBuilder.Entity<ChapterComment>()
            .HasMany(chapterComment => chapterComment.Replies)
            .WithOne(chapterComment => chapterComment.ReplyToComment)
            .HasForeignKey(chapterComment => chapterComment.ReplyToCommentId);

        modelBuilder.Entity<ChapterComment>()
            .HasMany(chapterComment => chapterComment.Dislikes)
            .WithOne(commentDislike => commentDislike.Comment)
            .HasForeignKey(commentDislike => commentDislike.CommentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ChapterComment>()
            .HasMany(chapterComment => chapterComment.Likes)
            .WithOne(commentLike => commentLike.Comment)
            .HasForeignKey(commentLike => commentLike.CommentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ChapterDislike>()
            .HasKey(dislike => new { dislike.ChapterId, dislike.UserId });

        modelBuilder.Entity<ChapterLike>()
            .HasKey(like => new { like.ChapterId, like.UserId });

        modelBuilder.Entity<CommentDislike<ChapterComment>>()
            .HasKey(commentDislike => new { commentDislike.CommentId, commentDislike.UserId });

        modelBuilder.Entity<CommentLike<ChapterComment>>()
            .HasKey(commentLike => new { commentLike.CommentId, commentLike.UserId });

        modelBuilder.Entity<Tag>()
            .HasMany(tag => tag.AssignedTags)
            .WithOne(assignedTag => assignedTag.Tag)
            .HasForeignKey(assignedTag => assignedTag.TagId);

        modelBuilder.Entity<Title>()
            .HasMany(title => title.Tags)
            .WithOne(assignedTag => assignedTag.Title)
            .HasForeignKey(assignedTag => assignedTag.TitleId);

        modelBuilder.Entity<Title>()
            .HasMany(title => title.Saved)
            .WithOne(savedTitle => savedTitle.Title)
            .HasForeignKey(savedTitle => savedTitle.TitleId);

        modelBuilder.Entity<Title>()
            .HasMany(title => title.Comments)
            .WithOne(titleComment => titleComment.Title)
            .HasForeignKey(titleComment => titleComment.TitleId);

        modelBuilder.Entity<Title>()
            .HasMany(title => title.Dislikes)
            .WithOne(dislike => dislike.Title)
            .HasForeignKey(dislike => dislike.TitleId);

        modelBuilder.Entity<Title>()
            .HasMany(title => title.Likes)
            .WithOne(like => like.Title)
            .HasForeignKey(like => like.TitleId);

        modelBuilder.Entity<Title>()
            .HasMany(title => title.Volumes)
            .WithOne(volume => volume.Title)
            .HasForeignKey(volume => volume.TitleId);

        modelBuilder.Entity<TitleComment>()
            .HasMany(titleComment => titleComment.Replies)
            .WithOne(titleComment => titleComment.ReplyToComment)
            .HasForeignKey(titleComment => titleComment.ReplyToCommentId);

        modelBuilder.Entity<TitleComment>()
            .HasMany(titleComment => titleComment.Dislikes)
            .WithOne(commentDislike => commentDislike.Comment)
            .HasForeignKey(commentDislike => commentDislike.CommentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TitleComment>()
            .HasMany(titleComment => titleComment.Likes)
            .WithOne(commentLike => commentLike.Comment)
            .HasForeignKey(commentLike => commentLike.CommentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CommentDislike<TitleComment>>()
            .HasKey(commentDislike => new { commentDislike.CommentId, commentDislike.UserId });

        modelBuilder.Entity<CommentLike<TitleComment>>()
            .HasKey(commentLike => new { commentLike.CommentId, commentLike.UserId });

        modelBuilder.Entity<TitleDislike>()
            .HasKey(dislike => new { dislike.TitleId, dislike.UserId });

        modelBuilder.Entity<TitleLike>()
            .HasKey(like => new { like.TitleId, like.UserId });

        modelBuilder.Entity<TitleSaved>()
            .HasKey(saved => new { saved.UserId, saved.TitleId });

        modelBuilder.Entity<TranslationTeam>()
            .HasMany(translationTeam => translationTeam.Chapters)
            .WithOne(chapter => chapter.TranslationTeam)
            .HasForeignKey(chapter => chapter.TranslationTeamId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TranslationTeam>()
            .HasMany(translationTeam => translationTeam.Members)
            .WithOne(translationTeamMember => translationTeamMember.Team)
            .HasForeignKey(translationTeamMember => translationTeamMember.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TranslationTeamMember>()
            .HasKey(translationTeamMember => new { translationTeamMember.TeamId, translationTeamMember.UserId });

        modelBuilder.Entity<Volume>()
            .HasMany(volume => volume.Chapters)
            .WithOne(chapter => chapter.Volume)
            .HasForeignKey(chapter => chapter.VolumeId);

        modelBuilder.Entity<User>()
            .HasOne(user => user.TranslationTeamOwned)
            .WithOne(translationTeam => translationTeam.Owner)
            .HasForeignKey<TranslationTeam>(translationTeam => translationTeam.OwnerId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.TranslationTeamsMember)
            .WithOne(translationTeamMember => translationTeamMember.User)
            .HasForeignKey(translationTeamMember => translationTeamMember.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasMany(user => user.TitleCommentDislikes)
            .WithOne(titleCommentDislike => titleCommentDislike.User)
            .HasForeignKey(titleCommentDislike => titleCommentDislike.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(user => user.TitleCommentLikes)
            .WithOne(titleCommentLike => titleCommentLike.User)
            .HasForeignKey(titleCommentLike => titleCommentLike.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(user => user.TitleComments)
            .WithOne(titleComment => titleComment.Author)
            .HasForeignKey(titleComment => titleComment.AuthorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.TitleDislikes)
            .WithOne(titleDislike => titleDislike.User)
            .HasForeignKey(titleDislike => titleDislike.UserId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.TitleLikes)
            .WithOne(titleLike => titleLike.User)
            .HasForeignKey(titleLike => titleLike.UserId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.SavedTitles)
            .WithOne(savedTitle => savedTitle.User)
            .HasForeignKey(savedTitle => savedTitle.UserId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.ChapterCommentDislikes)
            .WithOne(chapterCommentDislike => chapterCommentDislike.User)
            .HasForeignKey(chapterCommentDislike => chapterCommentDislike.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(user => user.ChapterCommentLikes)
            .WithOne(chapterCommentLike => chapterCommentLike.User)
            .HasForeignKey(chapterCommentLike => chapterCommentLike.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(user => user.ChapterComments)
            .WithOne(chapterComment => chapterComment.Author)
            .HasForeignKey(chapterComment => chapterComment.AuthorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.ChapterDislikes)
            .WithOne(chapterDislike => chapterDislike.User)
            .HasForeignKey(chapterDislike => chapterDislike.UserId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.ChapterLikes)
            .WithOne(chapterLike => chapterLike.User)
            .HasForeignKey(chapterLike => chapterLike.UserId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.Notifications)
            .WithOne(notification => notification.User)
            .HasForeignKey(notification => notification.UserId);

        base.OnModelCreating(modelBuilder);
    }
}

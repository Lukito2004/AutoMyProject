using AutoMapper;
using AutoMy.Database;
using AutoMy.DomainModels;
using AutoMy.Interfaces;
using AutoMy.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoMy.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper mapper;
        private readonly AutoMyDBContext database;
        public PostService(IMapper _mapper, AutoMyDBContext _database)
        {
            mapper = _mapper;
            database = _database;
        }
        public void AddPost(PostDTO post)
        {

            database.Posts.Add(mapper.Map<Post>(post));
            database.SaveChanges();
        }

        public bool AddReport(ReportDTO report)
        {
            Report reportAboutThisPost = database.Reports.FirstOrDefault(o => o.PostId == report.PostId);
            if (reportAboutThisPost != null && reportAboutThisPost.SenderAccountId == report.SenderAccountId)
                return false;
            else
            {
                database.Reports.Add(mapper.Map<Report>(report));
                database.SaveChanges();
                return true;
            }
        }

        public IEnumerable<ReportDTO> GetAllReports() => mapper.Map<IEnumerable<ReportDTO>>(database.Reports);

        public void RemoveReportWithId(int id)
        {
            database.Reports.Remove(database.Reports.FirstOrDefault(o => o.Id == id));
            database.SaveChanges();
        }

        public IEnumerable<PostDTO> GetAllPosts() => mapper.Map<IEnumerable<PostDTO>>(database.Posts);

        public IEnumerable<PostDTO> GetPostersPosts(string PosterId) => mapper.Map<IEnumerable<PostDTO>>(database.Posts.Where(o => o.AccountId == PosterId));

        public PostDTO GetPostWithId(int id) => mapper.Map<PostDTO>(database.Posts.FirstOrDefault(o => o.Id == id));

        public void RemovePostById(int id)     
        {
            database.Posts.Remove(database.Posts.FirstOrDefault(o => o.Id == id));
            database.SaveChanges();
        }

        public void UpdatePost(PostDTO post)
        {
            database.Posts.Update(mapper.Map<Post>(post));
            database.SaveChanges();
        }

        public void RemoveAllReportsFromThisPost(int postId)
        {
            database.Reports.RemoveRange(database.Reports.Where(o => o.PostId == postId));
            database.SaveChanges();
        }
    }
}

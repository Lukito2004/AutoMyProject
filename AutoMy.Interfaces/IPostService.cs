using AutoMy.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.Interfaces
{
    public interface IPostService
    {
        PostDTO GetPostWithId(int id);
        void AddPost(PostDTO post);
        void RemovePostById(int id);
        void UpdatePost(PostDTO post);
        IEnumerable<PostDTO> GetPostersPosts (string PosterId);
        IEnumerable<PostDTO> GetAllPosts();
        bool AddReport(ReportDTO report);
        void RemoveReportWithId(int id);
        IEnumerable<ReportDTO> GetAllReports();
        void RemoveAllReportsFromThisPost(int postId);
    }
}

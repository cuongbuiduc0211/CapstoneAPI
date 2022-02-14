using AutoMapper;
using DatabaseAccess.Entities;
using DatabaseAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models;

namespace Services.Services
{
    public class PostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateNewPost(PostItem postItem)
        {
            Post existedPost = await _unitOfWork.PostRepository.GetFirstOrDefault(
                                    q => q.Title.ToLower().Equals(postItem.Title));
            if (existedPost != null)
            {
                return false;
            }
            else
            {
                Post newPost = _mapper.Map<Post>(postItem);
                newPost.CreatedDate = DateTime.Now;
                newPost.Status = (int)PostStatus.Publish;
                await _unitOfWork.PostRepository.Add(newPost);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }
        public async Task<int> CountPostsByMonth(DateTime date)
        {
            var count = (await _unitOfWork.PostRepository.GetAll(
                q => q.CreatedDate.Month == date.Month && q.CreatedDate.Year == date.Year)).Count();
            return count;
        }

        public async Task<IEnumerable<PostListResponse>> GetAllPosts()
        {
            List<Post> listDB = (await _unitOfWork.PostRepository.GetAll(
                null, o => o.OrderByDescending(s => s.CreatedDate))).ToList();
            List<PostListResponse> listResponse = new List<PostListResponse>();
            foreach (var item in listDB)
            {
                var creator = await _unitOfWork.UserRepository.GetFirstOrDefault(
                    q => q.Id == item.CreatedBy);
                listResponse.Add(new PostListResponse()
                {
                    Id = item.Id,
                    FeaturedImage = item.FeaturedImage,
                    Title = item.Title,
                    Overview = item.Overview,
                    Type = item.Type,
                    CreatorImage = creator.Image,
                    CreatorName = creator.FullName,
                    BrandId = item.BrandId,
                    Status = item.Status
                });
            }
            return listResponse;
        }
        public async Task<IEnumerable<PostListResponse>> GetAllPostsByType(PostType postType)
        {
            //await _unitOfWork.PostRepository.GetAll(
            //    q => q.Type == (int)postType,
            //    o => o.OrderByDescending(s => s.CreatedDate));
            List<Post> listDB = (await _unitOfWork.PostRepository.GetAll(
                q => q.Type == (int)postType,
                o => o.OrderByDescending(s => s.CreatedDate))).ToList();
            List<PostListResponse> listResponse = new List<PostListResponse>();
            foreach (var item in listDB)
            {
                var creator = await _unitOfWork.UserRepository.GetFirstOrDefault(
                    q => q.Id == item.CreatedBy);
                listResponse.Add(new PostListResponse()
                {
                    Id = item.Id,
                    FeaturedImage = item.FeaturedImage,
                    Title = item.Title,
                    Overview = item.Overview,
                    Type = item.Type,
                    CreatorImage = creator.Image,
                    CreatorName = creator.FullName,
                    BrandId = item.BrandId,
                    Status = item.Status
                });
            }
            return listResponse;
        }
        public async Task<IEnumerable<PostListResponse>> GetAllPostsByKeyword(string keyword)
        {
            //return await _unitOfWork.PostRepository.GetAll(q => 
            //    q.Title.Contains(keyword) || q.Contents.Contains(keyword),
            //    o => o.OrderByDescending(s => s.CreatedDate));
            List<Post> listDB = (await _unitOfWork.PostRepository.GetAll(
                q => q.Title.Contains(keyword) || q.Contents.Contains(keyword),
                o => o.OrderByDescending(s => s.CreatedDate))).ToList();
            List<PostListResponse> listResponse = new List<PostListResponse>();
            foreach (var item in listDB)
            {
                var creator = await _unitOfWork.UserRepository.GetFirstOrDefault(
                    q => q.Id == item.CreatedBy);
                listResponse.Add(new PostListResponse()
                {
                    Id = item.Id,
                    FeaturedImage = item.FeaturedImage,
                    Title = item.Title,
                    Overview = item.Overview,
                    Type = item.Type,
                    CreatorImage = creator.Image,
                    CreatorName = creator.FullName,
                    BrandId = item.BrandId,
                    Status = item.Status
                });
            }
            return listResponse;
        }
        public async Task<IEnumerable<PostListResponse>> GetAllPostsByBrand(string brandId)
        {
            //return await _unitOfWork.PostRepository.GetAll(q =>
            //    q.BrandId == brandId,
            //    o => o.OrderByDescending(s => s.CreatedDate));
            List<Post> listDB = (await _unitOfWork.PostRepository.GetAll(
                q => q.BrandId == brandId)).ToList();
            List<PostListResponse> listResponse = new List<PostListResponse>();
            foreach (var item in listDB)
            {
                var creator = await _unitOfWork.UserRepository.GetFirstOrDefault(
                    q => q.Id == item.CreatedBy);
                listResponse.Add(new PostListResponse()
                {
                    Id = item.Id,
                    FeaturedImage = item.FeaturedImage,
                    Title = item.Title,
                    Overview = item.Overview,
                    Type = item.Type,
                    CreatorImage = creator.Image,
                    CreatorName = creator.FullName,
                    BrandId = item.BrandId,
                    Status = item.Status
                });
            }
            return listResponse;
        }
        public async Task<PostResponse> GetPostById(int id)
        {
            Post postDB = await _unitOfWork.PostRepository.GetFirstOrDefault(
                q => q.Id == id);
            User creator = await _unitOfWork.UserRepository.GetFirstOrDefault(
                    q => q.Id == postDB.CreatedBy);
            Brand brand = await _unitOfWork.BrandRepository.GetFirstOrDefault(
                    q => q.Id == postDB.BrandId);
            PostResponse postResponse = new PostResponse()
            {
                Id = postDB.Id,
                FeaturedImage = postDB.FeaturedImage,
                Title = postDB.Title,
                Overview = postDB.Overview,
                Contents = postDB.Contents,
                Type = postDB.Type,
                BrandId = postDB.BrandId,
                BrandImage = brand.Image,
                BrandName = brand.Name,
                CreatedBy = postDB.CreatedBy,
                CreatorImage = creator.Image,
                CreatorName = creator.FullName,
                CreatedDate = postDB.CreatedDate,
                Status = postDB.Status
            };
            return postResponse;
        }

        public async Task<bool> UpdatePost(int id, PostItem postItem)
        {
            Post existedPost = await _unitOfWork.PostRepository.GetFirstOrDefault(q => q.Id == id);
            if (existedPost != null)
            {
                existedPost = _mapper.Map<PostItem, Post>(postItem);
                existedPost.ModifiedDate = DateTime.Now;
                existedPost.Id = id;
                _unitOfWork.PostRepository.Update(existedPost);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ChangePostStatus(int id, PostStatus postStatus)
        {
            Post post = await _unitOfWork.PostRepository.Get(id);
            if (post != null)
            {
                post.Status = (int)postStatus;
                _unitOfWork.PostRepository.Update(post);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

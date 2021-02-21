using System;
using ApiWithToken.Domain.Entities;
using ApiWithToken.Domain.Repository;
using ApiWithToken.Domain.Response;
using ApiWithToken.Domain.Services;
using ApiWithToken.Domain.UnitOfWork;

namespace ApiWithToken.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public UserResponse Add(User user)
        {
            try
            {
                _userRepository.AddUser(user);
                _unitOfWork.Complate();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı eklenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public UserResponse FindById(int userId)
        {
            try
            {
                var user = _userRepository.FindById(userId);
                return user == null ? new UserResponse("Kullanıcı bulunamadı") : new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public UserResponse FindEmailAndPassword(string email, string password)
        {
            try
            {
                var user = _userRepository.FindByEmailAndPassword(email, password);
                return user == null ? new UserResponse("Kullanıcı bulunamadı") : new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            try
            {
                _userRepository.SaveRefreshToken(userId, refreshToken);

                _unitOfWork.Complate();
            }
            catch (Exception)
            {
                //loglama yapılacaktır..
            }
        }

        public UserResponse GetUserRefreshToken(string refreshToken)
        {
            try
            {
                var user = _userRepository.GetUserRefreshToken(refreshToken);
                return user == null ? new UserResponse("Kullanıcı bulunamadı") : new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public void RemoveRefreshToken(User user)
        {
            try
            {
                _userRepository.RemoveRefreshToken(user);
                _unitOfWork.Complate();
                
            }
            catch (Exception ex)
            {
                // Console.WriteLine(e);
                // throw;
            }
        }
    }
}
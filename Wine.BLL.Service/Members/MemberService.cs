using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Wine.IDAL.IRepository.Members;
using Wine.Infa.Dto.Members;
using Wine.Infa.Entity.Members;

namespace Wine.BLL.Service.Members
{
    public class MemberService
    {
        private readonly IMemberRepository _repo;

        public MemberService(IMemberRepository repo)
        {
            _repo = repo;
        }

        public bool Login(string account, string password)
        {
            //判斷帳號密碼
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var entity = _repo.Login(account, password);
            if (entity == null)
            {
                return false;
            }

            return true;
        }

        public string Register(MemberRegisterDto dto)
        {
            try
            {
                DateOnly dateofBirth = DateOnly.FromDateTime(dto.DateOfBirth);
                if (dto.Password == dto.ConfirmPassword)
                {
                    MemberRegisterEntity entity = new MemberRegisterEntity
                    {
                        Id = dto.Id,
                        Account = dto.Account,
                        Password = dto.Password,
                        Name = dto.Name,
                        Email = dto.Email,
                        Phone = dto.Phone,
                        DateOfBirth = dateofBirth
                    };

                    //判斷帳號是否存在以及手機年齡符合規定
                    var isExist = _repo.GetMember(entity.Account);
                    if (isExist == null && Check(entity.Phone, entity.DateOfBirth, entity.Email))
                    {
                        var result = _repo.Register(entity);
                        if (result != null)
                        {
                            return "註冊成功";
                        }
                        return "註冊失敗";
                    }
                }
            }
            catch
            {
                return "註冊失敗";
            }
            return "註冊失敗";
        }

        public void Update(MemberRegisterDto dto)
        {
            DateOnly dateofBirth = DateOnly.FromDateTime(dto.DateOfBirth);
            if (dto.Password == dto.ConfirmPassword)
            {
                MemberRegisterEntity entity = new MemberRegisterEntity
                {
                    Id = dto.Id,
                    Account = dto.Account,
                    Password = dto.Password,
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    DateOfBirth = dateofBirth
                };

                if (Check(entity.Phone, entity.DateOfBirth, entity.Email))
                {
                    _repo.Update(entity);
                }
            }
        }

        //驗證手機為09開頭以及生日大於18歲以及Email格式
        private bool Check(string phone, DateOnly dateOfBirth, string email)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - dateOfBirth.Year;

            // 如果生日還沒到當前年份的今天，年齡要減一歲
            if (dateOfBirth > today.AddYears(-age))
            {
                age--;
            }

            // 驗證手機
            bool isPhoneValid = phone.Length == 10 && phone.StartsWith("09");

            // 驗證年齡
            bool isAgeValid = age >= 18;

            // 驗證Email
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool isEmailValid = Regex.IsMatch(email, pattern);

            //同時驗證手機年齡Email
            return isPhoneValid && isAgeValid && isEmailValid;
        }      
    }
}

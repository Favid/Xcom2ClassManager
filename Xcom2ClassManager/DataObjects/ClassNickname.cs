using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager.DataObjects
{
    public class ClassNickname
    {
        public string nickname { get; set; }
        public NicknameGender gender { get; set; }

        public ClassNickname()
        {
            nickname = "";
            gender = NicknameGender.Unisex;
        }

        public ClassNickname(ClassNickname other)
        {
            nickname = other.nickname;
            gender = other.gender;
        }

        public ClassNickname(string nickname, NicknameGender gender)
        {
            this.nickname = nickname;
            this.gender = gender;
        }

        public ClassNickname(string nickname, string genderString)
        {
            this.nickname = nickname;

            if (genderString.Equals(NicknameGender.Unisex.ToString()))
            {
                this.gender = NicknameGender.Unisex;
            }
            else if (genderString.Equals(NicknameGender.Male.ToString()))
            {
                this.gender = NicknameGender.Male;
            }
            else if (genderString.Equals(NicknameGender.Female.ToString()))
            {
                this.gender = NicknameGender.Female;
            }
        }

        public string[] getListViewStringArray()
        {
            return new string[2] { nickname, gender.ToString() };
        }
    }
}

using portfolio.Models;

namespace portfolio.Interfaces;

interface IPersonalInfo
{
    Task<PersonalInfoModel> GetPersonalInfo();
    Task<PersonalInfoModel> UpdateTitle(UpdateTitleModel model);
    Task<PersonalInfoModel> UpdateBio(UpdateBioModel model);
    Task<PersonalInfoModel> UploadImage(string image);
}

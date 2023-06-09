using System.Collections.Generic;
using WoWonderClient.Classes.Call;
using WoWonderClient.Classes.Global;
using WoWonderClient.Classes.Message;

namespace WoWonder.Helpers.Model
{
    public class Classes
    {
        public class Categories
        {
            public string CategoriesId { get; set; }
            public string CategoriesName { get; set; }
            public string CategoriesColor { get; set; }
            public List<SubCategories> SubList  { get; set; }
        }

        public class CallUser
        {
            public string VideoCall { get; set; }

            public string UserId { get; set; }
            public string Avatar { get; set; }
            public string Name { get; set; }

            //Data
            public string Id { get; set; } // call_id
            public string AccessToken { get; set; }
            public string AccessToken2 { get; set; }
            public string FromId { get; set; }
            public string ToId { get; set; }
            public string Active { get; set; }
            public string Called { get; set; }
            public string Time { get; set; }
            public string Declined { get; set; }
            public string Url { get; set; }
            public string Status { get; set; }
            public string RoomName { get; set; }
            public TypeCall Type { get; set; }

            //Style       
            public string TypeIcon { get; set; }
            public string TypeColor { get; set; }
        }
         
        public class SharedFile
        {
            public string FileName { set; get; }
            public string FileType { set; get; }
            public string FileDate { set; get; }
            public string FilePath { set; get; }
            public string FileExtension { set; get; }
            public string ImageExtra { set; get; }

        }

        public class Gender
        {
            public string GenderId { get; set; }
            public string GenderName { get; set; }
            public string GenderColor { get; set; }
            public bool GenderSelect { get; set; }
        }

        public class OptionLastChat
        {
            public string ChatId { set; get; }
            public string PageId { set; get; }
            public string GroupId { set; get; }
            public string UserId { set; get; }
            public string Name { set; get; }
            public string ChatType { set; get; }  
        }
         
        public class LastChatArchive : OptionLastChat
        { 
            public string IdLastMessage { set; get; }

            public ChatObject LastChat { get; set; }
        }
         
        public class LastChatsClass
        {
            public ItemType Type { set; get; }

            public List<UserDataObject> UserRequestList { get; set; }
            public List<GroupChatRequest> GroupRequestList { get; set; }
            public ChatObject LastChat { get; set; }
            public string CountArchive { get; set; }

        }
         
        public class StorageTypeSelectClass
        {
            public int Id { set; get; }
            public string Type { set; get; }
            public string Text { get; set; }
            public bool Value { get; set; } 

        }
         
        public class ItemOptionObject
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public int Icon { get; set; }
            public string Color { get; set; }
        }
           
        public class AddNewChatObject
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public ItemType Type { get; set; }
            public UserDataObject User { get; set; }
        }
            
        public enum ItemType
        {
            LastChatNewV = 20202,
            EmptyPage = 20207,

            AddGroup = 20208,
            AddCall = 20209,
            User = 20210,
        }


        public class ModelsWalkTroutPager
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public int Image { get; set; }
        }
    }
      
    public enum SystemCall
    {
        Agora,
        Twilio
    }
     
    public enum EnableCall
    {
        AudioAndVideo,
        OnlyAudio,
        OnlyVideo,
        Disable
    }
     
    public enum ToastTheme
    {
        Default,
        Custom,
    }
    
    public enum ChatTheme
    {
        Default,
        Tokyo,
    }
    
    public enum ColorMessageTheme
    {
        Default,
        Gradient,
    }
      
    public enum PostButtonSystem
    { 
        ReactionDefault,
        ReactionSubShine
    }
     
    public enum ShowAds
    {
        AllUsers,
        UnProfessional,
    }

    public enum TabTheme
    {
        Dark,
        Light,
    }
}
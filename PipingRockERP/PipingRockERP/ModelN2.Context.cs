﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PipingRockERP
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PipingRockNewEntities : DbContext
    {
        public PipingRockNewEntities()
            : base("name=PipingRockNewEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Alergen> Alergens { get; set; }
        public virtual DbSet<Bottle> Bottles { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<FinishedGood> FinishedGoods { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemForm> ItemForms { get; set; }
        public virtual DbSet<ItemStatu> ItemStatus { get; set; }
        public virtual DbSet<ItemType> ItemTypes { get; set; }
        public virtual DbSet<ItemTypeDetail> ItemTypeDetails { get; set; }
        public virtual DbSet<PackagingLevel> PackagingLevels { get; set; }
        public virtual DbSet<QcTest> QcTests { get; set; }
        public virtual DbSet<QuarantineType> QuarantineTypes { get; set; }
        public virtual DbSet<Ref_GDSN_UoM> Ref_GDSN_UoM { get; set; }
        public virtual DbSet<ReportSort> ReportSorts { get; set; }
        public virtual DbSet<StorageCondition> StorageConditions { get; set; }
        public virtual DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<User_UserRole> User_UserRole { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserRole_UserRoleAuthority> UserRole_UserRoleAuthority { get; set; }
        public virtual DbSet<UserRoleAuthority> UserRoleAuthorities { get; set; }
        public virtual DbSet<ItemView> ItemViews { get; set; }
        public virtual DbSet<QcTestView> QcTestViews { get; set; }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int AddQuarantineType(string quarantineType)
        {
            var quarantineTypeParameter = quarantineType != null ?
                new ObjectParameter("QuarantineType", quarantineType) :
                new ObjectParameter("QuarantineType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddQuarantineType", quarantineTypeParameter);
        }
    
        public virtual int AddStorageCondition(string storageCondition, string storageConditionDescription)
        {
            var storageConditionParameter = storageCondition != null ?
                new ObjectParameter("StorageCondition", storageCondition) :
                new ObjectParameter("StorageCondition", typeof(string));
    
            var storageConditionDescriptionParameter = storageConditionDescription != null ?
                new ObjectParameter("StorageConditionDescription", storageConditionDescription) :
                new ObjectParameter("StorageConditionDescription", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddStorageCondition", storageConditionParameter, storageConditionDescriptionParameter);
        }
    
        public virtual ObjectResult<GetAllQuarantineTypes_Result> GetAllQuarantineTypes()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllQuarantineTypes_Result>("GetAllQuarantineTypes");
        }
    
        public virtual ObjectResult<GetAllStorageConditions_Result> GetAllStorageConditions()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllStorageConditions_Result>("GetAllStorageConditions");
        }
    
        public virtual ObjectResult<GetQuarantineTypeByID_Result> GetQuarantineTypeByID(Nullable<int> quarantineTypeID)
        {
            var quarantineTypeIDParameter = quarantineTypeID.HasValue ?
                new ObjectParameter("QuarantineTypeID", quarantineTypeID) :
                new ObjectParameter("QuarantineTypeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetQuarantineTypeByID_Result>("GetQuarantineTypeByID", quarantineTypeIDParameter);
        }
    
        public virtual ObjectResult<GetStorageConditionByID_Result> GetStorageConditionByID(Nullable<int> storageConditionID)
        {
            var storageConditionIDParameter = storageConditionID.HasValue ?
                new ObjectParameter("StorageConditionID", storageConditionID) :
                new ObjectParameter("StorageConditionID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetStorageConditionByID_Result>("GetStorageConditionByID", storageConditionIDParameter);
        }
    
        public virtual int UpdateQuarantineTypeByID(Nullable<int> quarantineTypeID, string quarantineType)
        {
            var quarantineTypeIDParameter = quarantineTypeID.HasValue ?
                new ObjectParameter("QuarantineTypeID", quarantineTypeID) :
                new ObjectParameter("QuarantineTypeID", typeof(int));
    
            var quarantineTypeParameter = quarantineType != null ?
                new ObjectParameter("QuarantineType", quarantineType) :
                new ObjectParameter("QuarantineType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateQuarantineTypeByID", quarantineTypeIDParameter, quarantineTypeParameter);
        }
    
        public virtual int UpdateStorageConditionByID(Nullable<int> storageConditionID, string storageCondition, string storageConditionDescription)
        {
            var storageConditionIDParameter = storageConditionID.HasValue ?
                new ObjectParameter("StorageConditionID", storageConditionID) :
                new ObjectParameter("StorageConditionID", typeof(int));
    
            var storageConditionParameter = storageCondition != null ?
                new ObjectParameter("StorageCondition", storageCondition) :
                new ObjectParameter("StorageCondition", typeof(string));
    
            var storageConditionDescriptionParameter = storageConditionDescription != null ?
                new ObjectParameter("StorageConditionDescription", storageConditionDescription) :
                new ObjectParameter("StorageConditionDescription", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateStorageConditionByID", storageConditionIDParameter, storageConditionParameter, storageConditionDescriptionParameter);
        }
    
        public virtual int AddRoleUserID(Nullable<int> userID, Nullable<int> userRoleID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var userRoleIDParameter = userRoleID.HasValue ?
                new ObjectParameter("UserRoleID", userRoleID) :
                new ObjectParameter("UserRoleID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddRoleUserID", userIDParameter, userRoleIDParameter);
        }
    
        public virtual int AddUser(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddUser", userNameParameter);
        }
    
        public virtual ObjectResult<GetAllNotActiveUserRoles_Result> GetAllNotActiveUserRoles(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllNotActiveUserRoles_Result>("GetAllNotActiveUserRoles", userIdParameter);
        }
    
        public virtual ObjectResult<GetAllRoles_Result> GetAllRoles()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllRoles_Result>("GetAllRoles");
        }
    
        public virtual ObjectResult<GetAllUserRoles_Result> GetAllUserRoles(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllUserRoles_Result>("GetAllUserRoles", userIdParameter);
        }
    
        public virtual ObjectResult<GetAllUsers_Result> GetAllUsers(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllUsers_Result>("GetAllUsers", userNameParameter);
        }
    
        public virtual ObjectResult<GetUser_Result> GetUser(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUser_Result>("GetUser", userNameParameter);
        }
    
        public virtual ObjectResult<GetUserByID_Result> GetUserByID(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUserByID_Result>("GetUserByID", userIDParameter);
        }
    
        public virtual ObjectResult<GetUserDisabledRolesByID_Result> GetUserDisabledRolesByID(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUserDisabledRolesByID_Result>("GetUserDisabledRolesByID", userIDParameter);
        }
    
        public virtual ObjectResult<GetUserRolesByID_Result> GetUserRolesByID(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUserRolesByID_Result>("GetUserRolesByID", userIDParameter);
        }
    
        public virtual int RemoveRoleUserID(Nullable<int> userID, Nullable<int> userRoleID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var userRoleIDParameter = userRoleID.HasValue ?
                new ObjectParameter("UserRoleID", userRoleID) :
                new ObjectParameter("UserRoleID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RemoveRoleUserID", userIDParameter, userRoleIDParameter);
        }
    
        public virtual int UpdateUserByID(Nullable<int> userID, string userName, Nullable<int> userRoleID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var userRoleIDParameter = userRoleID.HasValue ?
                new ObjectParameter("UserRoleID", userRoleID) :
                new ObjectParameter("UserRoleID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateUserByID", userIDParameter, userNameParameter, userRoleIDParameter);
        }
    }
}
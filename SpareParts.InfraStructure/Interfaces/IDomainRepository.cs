using System;
using System.Collections.Generic;

namespace SpareParts.InfraStructure.Interfaces;

public interface IDomainRepository: IDisposable
{
    
}

public interface IDomainRepository<TModel>: IDomainRepository 
    where TModel : class
{
    bool SaveChanges();

    IEnumerable<TModel> GetAllModel();
    TModel GetById(int id);
    void CreateModel(TModel model);
    void UpdateModel(TModel model);
    void DeleteModel(TModel model);
}
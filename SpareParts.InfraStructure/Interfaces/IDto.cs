namespace SpareParts.InfraStructure.Interfaces;

public interface IDto
{
    
}
public class CreateDto<TModel> : IDto
    where TModel : class
{

}
public class ReadDto<TModel> : IDto 
    where TModel : class
{

}
public class UpdateDto<TModel>: IDto
    where TModel : class
{

}
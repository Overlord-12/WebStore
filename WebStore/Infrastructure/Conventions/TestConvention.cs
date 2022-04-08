using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebStore.Infrastructure.Conventions;

public class TestConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (controller.ControllerName == "Home")
        {
            //controller.Actions.Add(new ActionModel(s));
        }
    }
}
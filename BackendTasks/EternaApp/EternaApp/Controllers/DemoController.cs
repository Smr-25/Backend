using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EternaApp.Controllers;

public class DemoController(IConfiguration config,IOptions<GroupSettings> groupSettings) : Controller
{
    public IActionResult Index()
    {
        // var groupName = config["GroupName"];
        // var groupId = config["GroupId"];
        // var groupName = config.GetSection("GroupName").Value;
        // var groupId = config.GetSection("GroupId").Value;
        var groupName = groupSettings.Value.GroupName;
        var groupId = groupSettings.Value.GroupId;      
        return Content($"Group Name {groupName} Group Id {groupId}");
    }
}
using eCart_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using System.Linq;
using System.Dynamic;
using System.Runtime.CompilerServices;
using eCart_Backend;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<eCartDbContext>(builder.Configuration["eCartDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:5169")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});


var app = builder.Build();
//Add for Cors 
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//Check if User exist in the system
app.MapGet("/checkuser/{uid}", (eCartDbContext db, string uid) =>
{
    var user = db.Users.Where(x => x.Uid == uid).ToList();
    if (uid == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(user);
    }
});

//Create a User
app.MapPost("/api/user", (eCartDbContext db, User user) =>
{
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"/api/user/{user.Id}", user);
});

//Get user by id
app.MapGet("/api/user/{id}", (eCartDbContext db, int id) =>
{
    var user = db.Users.Single(u => u.Id == id);
    return user;
});

//Items
//Get item by id
app.MapGet("/api/item/{id}", (eCartDbContext db, int id) =>
{
    var user = db.Items.Single(i => i.Id == id);
    return user;
});

//Delete an Item
app.MapDelete("/api/item/{id}", (eCartDbContext db, int id) =>
{
    Item item = db.Items.SingleOrDefault(i => i.Id == id);
    if (item == null)
    {
        return Results.NotFound("Item is not found");
    }
    db.Items.Remove(item);
    db.SaveChanges();
    return Results.NoContent();

});

//Add an item
app.MapPost("/api/additem", (eCartDbContext db, Item item) =>
{
    db.Items.Add(item);
    db.SaveChanges();
    return Results.Created($"/api/items/{item.Id}", item);
});

//Add an item to an order
app.MapPost("/api/itemtoorder/{id}", (eCartDbContext db, int id, Item item) =>
{
    var order = db.Orders.Where(o => o.Id == id).Include(I => I.items).FirstOrDefault();
    if (order == null)
    {
        return Results.NotFound("Order not found");
    }

    order.items.Add(item);
    db.SaveChanges();
    return Results.Created($"/api/itemtoorder/{order.Id}", item);
});

//Delete an item from an order
app.MapDelete("/api/OrderItem/{orderId}/{itemId}", (eCartDbContext db, int orderId, int itemId) =>
{
    var order = db.Orders.Where(o => o.Id == orderId).Include(I => I.items).FirstOrDefault();
    var item = order.items.Where(i => i.Id == itemId).FirstOrDefault();
    if (order == null)
    {
        return Results.NotFound("Order not found");
    }

    order.items.Remove(item);
    db.SaveChanges();
    return Results.NoContent();
});


//Get all items
app.MapGet("/api/allItems", (eCartDbContext db) =>
{

    return db.Items;
}
);

// Orders
//View user's open orders
app.MapGet("/api/getUserOrders/{userId}", (eCartDbContext db, int userId) =>
{
    var userOrders = db.Orders.Where(o => o.UserId == userId).Include(x =>x.items).ToList();
    var openOrder = userOrders.Where(o => o.StatusId == 1).FirstOrDefault();
    var orderItems = openOrder.items.ToList();
    return orderItems.ToList();
});


//View user's orders
app.MapGet("/api/UserOrders/{userId}", (eCartDbContext db, int userId) =>
{
    var userOrders = db.Orders.Where(o => o.UserId == userId).Include(x => x.items).ToList();

    return userOrders.ToList();
});

//Update an order, checkout
app.MapPut("/api/closeOrder/{id}", (eCartDbContext db, int id, Order order) =>
{
    Order OrderToUpdate = db.Orders.SingleOrDefault(order => order.Id == id);
    if (OrderToUpdate == null)
    {
        return Results.NotFound();
    }
    OrderToUpdate.OrderDate = order.OrderDate;
    OrderToUpdate.OrderTotal = order.OrderTotal;
    OrderToUpdate.StatusId = order.StatusId;
    OrderToUpdate.PaymentId = order.PaymentId;
    OrderToUpdate.UserId = order.UserId;

    db.SaveChanges();
    return Results.NoContent();
});
//Delete an Order
app.MapDelete("/api/order/{id}", (eCartDbContext db, int id) =>
{
    Order order = db.Orders.SingleOrDefault(o => o.Id == id);
    if (order == null)
    {
        return Results.NotFound();
    }
    db.Orders.Remove(order);
    db.SaveChanges();
    return Results.NoContent();

});

//Create an Order 
app.MapPost("/api/CreateOrder", (eCartDbContext db, Order order) =>
{
    db.Orders.Add(order);
    db.SaveChanges();
    return Results.Created($"/api/products/{order.Id}", order);

});

//Get Order by User ID
app.MapGet("api/getUserOpenOrder/{id}", (eCartDbContext db, int id) =>
{
    var userOrder = db.Orders.Where(o => o.UserId == id).ToList();
    if(userOrder == null)
    {
        return Results.NotFound("No order found");
    }
    foreach (var order in userOrder)
    {
        if (order.StatusId == 1)
        {
            return Results.Ok(order);
        }

    }
    return Results.NotFound("No Open Order");

});


//Get all orders
app.MapGet("/api/allOrders", (eCartDbContext db) =>
{

    return db.Orders;
}
);

//Get all categories
app.MapGet("/api/allCategories", (eCartDbContext db) =>
{

    return db.Categories;
}
);

//Get Items by Category
app.MapGet("api/getCategoryItems/{id}", (eCartDbContext db, int id) =>
{
    var categoryItems = db.Items.Where(i => i.CategoryId == id);

    return categoryItems.ToList();
});

//Get all payments
app.MapGet("/api/allPayments", (eCartDbContext db) =>
{

    return db.Payments;
}
);

//View order's items
app.MapGet("/api/OrderDetails/{id}", (eCartDbContext db, int id) =>
{
    var getOrder = db.Orders.Where(o => o.Id == id).Include(x => x.items).FirstOrDefault();
    if (getOrder != null)
    {
        var items = getOrder.items.ToList();
        return Results.Ok(items);
    }
    else
    {
        return Results.NotFound();
    }

}
);

//update item
app.MapPut("/api/items/{id}", (eCartDbContext db, int id, Item item) =>
{
    Item itemToUpdate = db.Items.SingleOrDefault(product => product.Id == id);
    if (itemToUpdate == null)
    {
        return Results.NotFound();
    }
    itemToUpdate.Name = item.Name;
    itemToUpdate.Price = item.Price;
    itemToUpdate.Description = item.Description;
    itemToUpdate.Quantity = item.Quantity;
    itemToUpdate.Image = item.Image;
    itemToUpdate.CategoryId = item.CategoryId;
    itemToUpdate.UserId = item.UserId;


    db.SaveChanges();
    return Results.NoContent();
});

app.Run();

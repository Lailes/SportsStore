using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportsStore.Infrastructure;

namespace SportsStore.Models
{
    public class SessionCart : Cart
    {
        [JsonIgnore] public ISession Session { get; set; }

        public static Cart GetCart(IServiceProvider provider)
        {
            var session = provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            var cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveAll(Product product)
        {
            base.RemoveAll(product);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.SetJson("Cart", this);
        }

        public override void Remove(Product product)
        {
            base.Remove(product);
            Session.SetJson("Cart", this);
        }
    }
}
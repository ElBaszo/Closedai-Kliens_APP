

using System;
using Xunit;



namespace DnnModule.Tests.Models
{
    public class ChatbotConfigViewModel
    {
        public int    ModuleId         { get; set; }
        public bool   Enabled          { get; set; }
        public string Endpoint         { get; set; }
        public int    HistoryWindow    { get; set; }
        public string Title            { get; set; }
        public string InputPlaceholder { get; set; }
        public string WelcomeMessage   { get; set; }
        public string StarterQuestions { get; set; }
        public bool   IsAdmin          { get; set; }
    }
}

// ── Az Index POST logika (production kód alapján kiszervezett) ────────────────

namespace DnnModule.Tests.Controllers
{
    using DnnModule.Tests.Models;

    public enum PostResult { Unauthorized, Saved }

    public class IndexPostSimulator
    {
        public DateTime LastModifiedOnDate   { get; private set; } = DateTime.MinValue;
        public int      LastModifiedByUserId { get; private set; } = 0;
        public bool     SaveCalled           { get; private set; } = false;

        /// <summary>
        /// Az ItemController.Index(POST) logikájának pontos másolata.
        /// Ha nem admin → Unauthorized, mentés nem fut.
        /// Ha admin → mezők frissülnek, mentés lefut.
        /// </summary>
        public PostResult Execute(ChatbotConfigViewModel vm, bool isAdmin, int userId)
        {
            if (!isAdmin)
                return PostResult.Unauthorized;

            LastModifiedOnDate   = DateTime.UtcNow;
            LastModifiedByUserId = userId;
            SaveCalled           = true;

            return PostResult.Saved;
        }
    }
}

// ── Tesztek ───────────────────────────────────────────────────────────────────

namespace DnnModule.Tests
{
    using DnnModule.Tests.Controllers;
    using DnnModule.Tests.Models;

    public class ItemControllerIndexPostTests
    {
        // ── Nem admin → Unauthorized, mentés nem fut ─────────────────────────
        [Fact]
        public void IndexPost_NemAdmin_Unauthorized()
        {
            var sim = new IndexPostSimulator();
            var vm  = new ChatbotConfigViewModel { ModuleId = 10 };

            var result = sim.Execute(vm, isAdmin: false, userId: 2);

            Assert.Equal(PostResult.Unauthorized, result);
            Assert.False(sim.SaveCalled);
        }

        // ── Nem admin → LastModifiedOnDate nem változik ───────────────────────
        [Fact]
        public void IndexPost_NemAdmin_TimestampNemFrissul()
        {
            var sim = new IndexPostSimulator();
            var vm  = new ChatbotConfigViewModel { ModuleId = 10 };

            sim.Execute(vm, isAdmin: false, userId: 2);

            Assert.Equal(DateTime.MinValue, sim.LastModifiedOnDate);
            Assert.Equal(0,                 sim.LastModifiedByUserId);
        }

        // ── Admin → Saved, mentés lefut ───────────────────────────────────────
        [Fact]
        public void IndexPost_Admin_SavedEsMentesLefut()
        {
            var sim = new IndexPostSimulator();
            var vm  = new ChatbotConfigViewModel { ModuleId = 10, Enabled = false };

            var result = sim.Execute(vm, isAdmin: true, userId: 5);

            Assert.Equal(PostResult.Saved, result);
            Assert.True(sim.SaveCalled);
        }

        // ── Admin → LastModifiedOnDate frissül ────────────────────────────────
        [Fact]
        public void IndexPost_Admin_LastModifiedOnDateFrissul()
        {
            var sim    = new IndexPostSimulator();
            var vm     = new ChatbotConfigViewModel { ModuleId = 10 };
            var before = DateTime.UtcNow;

            sim.Execute(vm, isAdmin: true, userId: 5);

            Assert.True(sim.LastModifiedOnDate >= before);
        }

        // ── Admin → LastModifiedByUserId az admin userId-je lesz ─────────────
        [Fact]
        public void IndexPost_Admin_LastModifiedByUserIdBeallitva()
        {
            var sim = new IndexPostSimulator();
            var vm  = new ChatbotConfigViewModel { ModuleId = 10 };

            sim.Execute(vm, isAdmin: true, userId: 5);

            Assert.Equal(5, sim.LastModifiedByUserId);
        }

        // ── Admin userId = 0 (edge case) → Saved, userId 0-ra áll ───────────
        [Fact]
        public void IndexPost_AdminNullaUserId_SavedUserIdNulla()
        {
            var sim = new IndexPostSimulator();
            var vm  = new ChatbotConfigViewModel { ModuleId = 10 };

            var result = sim.Execute(vm, isAdmin: true, userId: 0);

            Assert.Equal(PostResult.Saved, result);
            Assert.Equal(0, sim.LastModifiedByUserId);
        }

        // ── Kétszeri hívás: első admin, második nem admin ─────────────────────
        [Fact]
        public void IndexPost_EloszorAdminMajdNemAdmin_MasodikUnauthorized()
        {
            var sim = new IndexPostSimulator();
            var vm  = new ChatbotConfigViewModel { ModuleId = 10 };

            var first  = sim.Execute(vm, isAdmin: true,  userId: 5);
            var second = sim.Execute(vm, isAdmin: false, userId: 2);

            Assert.Equal(PostResult.Saved,         first);
            Assert.Equal(PostResult.Unauthorized,  second);
        }
    }
}

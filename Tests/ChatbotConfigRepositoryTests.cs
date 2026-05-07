// NuGet: xunit + xunit.runner.visualstudio
// Nincs szükség project reference-re – minden definíció itt van.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

// ── Model ─────────────────────────────────────────────────────────────────────

namespace DnnModule.Tests.Models
{
    public class ChatbotConfig
    {
        public int      ConfigId             { get; set; }
        public int      ModuleId             { get; set; }
        public bool     Enabled              { get; set; }
        public string   Endpoint             { get; set; }
        public int      HistoryWindow        { get; set; }
        public string   Title                { get; set; }
        public string   InputPlaceholder     { get; set; }
        public string   WelcomeMessage       { get; set; }
        public string   StarterQuestions     { get; set; }
        public DateTime CreatedOnDate        { get; set; }
        public int      CreatedByUserId      { get; set; }
        public DateTime LastModifiedOnDate   { get; set; }
        public int      LastModifiedByUserId { get; set; }
    }
}

// ── In-memory repository (DataContext helyett) ────────────────────────────────

namespace DnnModule.Tests.Repositories
{
    using DnnModule.Tests.Models;

    public class TestableChatbotConfigRepository
    {
        private readonly List<ChatbotConfig> _db = new List<ChatbotConfig>();
        private int _nextId = 1;

        public int RecordCount => _db.Count;

        public ChatbotConfig GetByModuleId(int moduleId) =>
            _db.FirstOrDefault(c => c.ModuleId == moduleId);

        public ChatbotConfig GetOrCreateDefault(int moduleId, int userId)
        {
            var existing = GetByModuleId(moduleId);
            if (existing != null)
                return existing;

            var now = DateTime.UtcNow;
            var config = new ChatbotConfig
            {
                ModuleId         = moduleId,
                Enabled          = true,
                Endpoint         = "/closedai-api/question",
                HistoryWindow    = 6,
                Title            = "A te asszisztensed",
                InputPlaceholder = "Írd be a kérdésed...",
                WelcomeMessage   = "Szia! Miben segíthetek?",
                StarterQuestions = "Mik a legkelendőbb termékek?\nAjánlj nekem egy modellt!",
                CreatedOnDate    = now,
                CreatedByUserId  = userId,
                LastModifiedOnDate   = now,
                LastModifiedByUserId = userId
            };
            Insert(config);
            return GetByModuleId(moduleId);
        }

        public void Save(ChatbotConfig config)
        {
            var existing = GetByModuleId(config.ModuleId);
            if (existing == null)
            {
                Insert(config);
            }
            else
            {
                config.ConfigId        = existing.ConfigId;
                config.CreatedOnDate   = existing.CreatedOnDate;
                config.CreatedByUserId = existing.CreatedByUserId;
                Update(config);
            }
        }

        private void Insert(ChatbotConfig config)
        {
            config.ConfigId = _nextId++;
            _db.Add(config);
        }

        private void Update(ChatbotConfig config)
        {
            var idx = _db.FindIndex(c => c.ConfigId == config.ConfigId);
            if (idx >= 0) _db[idx] = config;
        }
    }
}

// ── Tesztek ───────────────────────────────────────────────────────────────────

namespace DnnModule.Tests
{
    using DnnModule.Tests.Models;
    using DnnModule.Tests.Repositories;

    public class ChatbotConfigRepositoryTests
    {
        // ── GetOrCreateDefault: üres DB → új rekord jön létre ────────────────
        [Fact]
        public void GetOrCreateDefault_UresDB_UjRekordJonLetre()
        {
            var repo = new TestableChatbotConfigRepository();

            var result = repo.GetOrCreateDefault(moduleId: 5, userId: 1);

            Assert.NotNull(result);
            Assert.Equal(1, repo.RecordCount);
        }

        // ── GetOrCreateDefault: alapértékek helyesek ──────────────────────────
        [Fact]
        public void GetOrCreateDefault_UresDB_AlapertekekHeljesek()
        {
            var repo = new TestableChatbotConfigRepository();

            var result = repo.GetOrCreateDefault(moduleId: 5, userId: 1);

            Assert.Equal(5,                       result.ModuleId);
            Assert.True(result.Enabled);
            Assert.Equal("/closedai-api/question", result.Endpoint);
            Assert.Equal(6,                        result.HistoryWindow);
            Assert.Equal(1,                        result.CreatedByUserId);
            Assert.NotEqual(default,               result.CreatedOnDate);
        }

        // ── GetOrCreateDefault: létező rekord → ugyanaz jön vissza ───────────
        [Fact]
        public void GetOrCreateDefault_LetezoRekord_UgyanazJonVisszaNemDuplikal()
        {
            var repo = new TestableChatbotConfigRepository();
            repo.GetOrCreateDefault(moduleId: 5, userId: 1);

            var result = repo.GetOrCreateDefault(moduleId: 5, userId: 99);

            Assert.Equal(1, repo.RecordCount);        // nincs duplikátum
            Assert.Equal(1, result.CreatedByUserId);  // eredeti user marad
        }

        // ── GetOrCreateDefault: kétszeri hívás → pontosan 1 rekord ──────────
        [Fact]
        public void GetOrCreateDefault_KetszeriHivas_CsakEgyRekord()
        {
            var repo = new TestableChatbotConfigRepository();
            repo.GetOrCreateDefault(moduleId: 10, userId: 1);
            repo.GetOrCreateDefault(moduleId: 10, userId: 1);

            Assert.Equal(1, repo.RecordCount);
        }

        // ── GetByModuleId: nem létező ID → null, nincs exception ─────────────
        [Fact]
        public void GetByModuleId_NemLetezoId_NullNemException()
        {
            var repo = new TestableChatbotConfigRepository();

            var result = repo.GetByModuleId(999999);

            Assert.Null(result);
        }

        // ── GetByModuleId: 0 ID → null (nem létező) ───────────────────────────
        [Fact]
        public void GetByModuleId_NullaId_Null()
        {
            var repo = new TestableChatbotConfigRepository();

            var result = repo.GetByModuleId(0);

            Assert.Null(result);
        }

        // ── Save: nincs rekord → INSERT, megjelenik a DB-ben ─────────────────
        [Fact]
        public void Save_NincsRekord_InsertFutLe()
        {
            var repo   = new TestableChatbotConfigRepository();
            var config = new ChatbotConfig { ModuleId = 42, Enabled = true, Title = "Új" };

            repo.Save(config);

            Assert.Equal(1, repo.RecordCount);
            Assert.Equal("Új", repo.GetByModuleId(42).Title);
        }

        // ── Save: meglévő rekord → UPDATE, értékek frissülnek ────────────────
        [Fact]
        public void Save_LetezoRekord_UpdateFutLeErtekekFrissulnek()
        {
            var repo = new TestableChatbotConfigRepository();
            repo.GetOrCreateDefault(moduleId: 42, userId: 1);

            repo.Save(new ChatbotConfig { ModuleId = 42, Enabled = false, Title = "Módosított" });

            var result = repo.GetByModuleId(42);
            Assert.Equal("Módosított", result.Title);
            Assert.False(result.Enabled);
            Assert.Equal(1, repo.RecordCount); // nem jött létre új sor
        }

        // ── Save: UPDATE → CreatedOnDate nem változhat ────────────────────────
        [Fact]
        public void Save_Update_CreatedOnDateNemValtozik()
        {
            var repo         = new TestableChatbotConfigRepository();
            var original     = repo.GetOrCreateDefault(moduleId: 42, userId: 1);
            var originalDate = original.CreatedOnDate;

            repo.Save(new ChatbotConfig
            {
                ModuleId      = 42,
                CreatedOnDate = new DateTime(2099, 1, 1), // szándékosan rossz dátum
                Title         = "Frissített"
            });

            Assert.Equal(originalDate, repo.GetByModuleId(42).CreatedOnDate);
        }

        // ── Save: UPDATE → CreatedByUserId nem változhat ─────────────────────
        [Fact]
        public void Save_Update_CreatedByUserIdNemValtozik()
        {
            var repo = new TestableChatbotConfigRepository();
            repo.GetOrCreateDefault(moduleId: 42, userId: 1);

            repo.Save(new ChatbotConfig { ModuleId = 42, CreatedByUserId = 99, Title = "X" });

            Assert.Equal(1, repo.GetByModuleId(42).CreatedByUserId); // marad az eredeti
        }

        // ── Save: UPDATE → ConfigId nem változhat ────────────────────────────
        [Fact]
        public void Save_Update_ConfigIdNemValtozik()
        {
            var repo     = new TestableChatbotConfigRepository();
            var original = repo.GetOrCreateDefault(moduleId: 42, userId: 1);
            var origId   = original.ConfigId;

            repo.Save(new ChatbotConfig { ModuleId = 42, Title = "Frissített" });

            Assert.Equal(origId, repo.GetByModuleId(42).ConfigId);
        }

        // ── Két különböző modul → két külön rekord ────────────────────────────
        [Fact]
        public void GetOrCreateDefault_KetKulonbosoModul_KetRekord()
        {
            var repo = new TestableChatbotConfigRepository();
            repo.GetOrCreateDefault(moduleId: 1, userId: 1);
            repo.GetOrCreateDefault(moduleId: 2, userId: 1);

            Assert.Equal(2, repo.RecordCount);
            Assert.NotNull(repo.GetByModuleId(1));
            Assert.NotNull(repo.GetByModuleId(2));
        }
    }
}

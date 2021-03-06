﻿
using System.Collections.Generic;
using System.Linq;
using BayiPuan.Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using BayiPuan.DataAccess.Abstract;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace BayiPuan.Business.Concrete.Managers
{
    public class ScoreManager : ManagerBase, IScoreService
    {
        private readonly IScoreDal _scoreDal;

        public ScoreManager(IScoreDal scoreDal)
        {
            _scoreDal = scoreDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Score> GetAll()
        {
            return _scoreDal.GetList();
        }

        public Score GetById(int scoreId)
        {
            return _scoreDal.Get(u => u.ScoreId == scoreId);
        }      

        //[FluentValidationAspect(typeof(ScoreValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Score Add(Score score)
        {
            return _scoreDal.Add(score);
        }
        //[FluentValidationAspect(typeof(ScoreValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Score score)
        {
              _scoreDal.Update(score);
        }

        public void Delete(Score score)
        {
            _scoreDal.Delete(score);
        }    

        public List<Score> GetByScore(int scoreId)
        {
            return _scoreDal.GetList(filter: t => t.ScoreId == scoreId).ToList();
        }
    }
}

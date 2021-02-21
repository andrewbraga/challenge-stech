using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.Dsl;
using AutoFixture.Kernel;

namespace Application.UnitTests
{
    public static class FixtureHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="countItens"></param>
        /// <param name="composer"></param>
        /// <returns></returns>
        public static IEnumerable<T> CreateCollection<T>(int countItens,
                                       Func<ICustomizationComposer<T>, ISpecimenBuilder> composer)
        {
            var fixture = new Fixture();

            fixture.Customize(composer);

            return fixture.CreateMany<T>(countItens);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="composer"></param>
        /// <returns></returns>
        public static T Create<T>(Func<ICustomizationComposer<T>, ISpecimenBuilder> composer)
        {
            var fixture = new Fixture();

            fixture.Customize(composer);

            return fixture.Create<T>();
        }
    }
}

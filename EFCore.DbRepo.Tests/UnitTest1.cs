using System;
using EFCore.DbRepo.Tests.ConcreteImplementations;
using Xunit;

namespace EFCore.DbRepo.Tests
{
    public class it_can_perform_basic_repository_manipulations : with_a_test_record_repo
    {
        [Fact]
        public void Test1() {
            var test = new Test() { NotName = "test"};
            using (var tx = _unitOfWork.BeginTransaction()) {

                _unitOfWork.Repository.Insert(test);

                tx.Commit();
            }

            Assert.NotEqual(Guid.Empty, test.Identifier);

        }
    }
}

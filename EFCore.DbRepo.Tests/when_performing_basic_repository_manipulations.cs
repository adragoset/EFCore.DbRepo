using System;
using EFCore.DbRepo.Tests.ConcreteImplementations;
using Xunit;

namespace EFCore.DbRepo.Tests
{
    public class when_performing_basic_repository_manipulations : with_a_test_record_repo
    {
        private Test test1;

        public when_performing_basic_repository_manipulations() {
            test1 = new Test() { NotName = "test"};
        }

        [Fact]
        public async void it_can_insert_a_record() {
            var test = new Test() { NotName = "test"};
            using (var tx = _unitOfWork.BeginTransaction()) {

                await _unitOfWork.Repository.Insert(test);

                tx.Commit();
            }

            Assert.NotEqual(Guid.Empty, test.Identifier);
        }

        [Fact]
        public async void it_can_get_a_record_by_id() {
           using (var tx = _unitOfWork.BeginTransaction()) {
                await _unitOfWork.Repository.Insert(test1);
                tx.Commit();
            }

           
            var searchResult = await _unitOfWork.Repository.GetById(test1.Identifier);

            Assert.Equal(test1.Identifier, searchResult.Identifier);
            Assert.Equal(test1.NotName, searchResult.NotName);
        }
    }
}

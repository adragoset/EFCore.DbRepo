using System;
using EFCore.DbRepo.Tests.ConcreteImplementations;
using Xunit;

namespace EFCore.DbRepo.Tests
{
    public class when_performing_basic_repository_manipulations : with_a_test_record_repo
    {
        private Test test1;

        public when_performing_basic_repository_manipulations():base() {
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

        [Fact]
        public async void it_can_update_a_record() {
           using (var tx = _unitOfWork.BeginTransaction()) {
                await _unitOfWork.Repository.Insert(test1);
                tx.Commit();
            }

           
            var searchResult = await _unitOfWork.Repository.GetById(test1.Identifier);

            using(var tx = _unitOfWork.BeginTransaction()) {
                searchResult.NotName = "New Not Name";

                _unitOfWork.Repository.Update(searchResult);
                tx.Commit();
            }

            searchResult = await _unitOfWork.Repository.GetById(test1.Identifier);

            Assert.Equal(test1.Identifier, searchResult.Identifier);
            Assert.Equal("New Not Name", searchResult.NotName);
        }

        [Fact]
        public async void it_can_delete_a_record() {
           using (var tx = _unitOfWork.BeginTransaction()) {
                await _unitOfWork.Repository.Insert(test1);
                tx.Commit();
            }

           
            var searchResult = await _unitOfWork.Repository.GetById(test1.Identifier);

            using(var tx = _unitOfWork.BeginTransaction()) {
                _unitOfWork.Repository.Delete(searchResult.Identifier);
                tx.Commit();
            }

            searchResult = await _unitOfWork.Repository.GetById(test1.Identifier);

            Assert.Null(searchResult);
        }
    }
}

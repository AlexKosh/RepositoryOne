(function () {
    'use strict';

    angular
        .module('Jos.store')
        .controller('ModalNewEmployeeController', NewEmployeeController);

    NewEmployeeController.$inject = ['$scope', '$modalInstance', 'dataService'];
    function NewEmployeeController($scope, $modalInstance, dataService) {
        {
            function Employee(e) {
                this.EmployeeId = null;
                this.Name = e.Name;
                this.Surname = e.Surname;
                this.HireDate = new Date();
                this.Birthday = e.Birthday;
                this.Address = e.Address;
                this.PhoneNumber = e.PhoneNumber;
                this.AlternatePhone = e.AlternatePhone;
                this.Skype = e.Skype;
                this.SocialNetwork = e.SocialNetwork;
                this.Email = e.Email;
                this.Speciality = e.Speciality;
                this.Description = e.Description;
                this.Gender = e.Gender;
            }

            //$scope.tempWorker = {};

            $scope.createNewEmpl = function () {
                var emp = new Employee($scope.tempWorker);
                dataService.postNewEmpl(emp, $scope.$parent);
                $modalInstance.close();
            };

            $scope.cancel = function () {
                $modalInstance.dismiss();
            };
        }
    }
})();
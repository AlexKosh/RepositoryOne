angular
    .module('Jos.store')
    .controller('ModalNewCustomerController', NewCustomerController);

NewCustomerController.$inject = ['$rootScope', '$scope', '$modalInstance', 'dataService'];
function NewCustomerController($rootScope, $scope, $modalInstance, dataService) {
    {
        function Customer(c) {
            this.CustomerId = null;
            this.Name = c.Name;
            this.Surname = c.Surname;
            this.Nickname = c.Nickname;
            this.Gender = c.Gender;
            this.Birthday = c.Birthday;
            this.Age = 0;

            this.FirstMet = new Date();

            this.Notations = c.Notations;
            this.locationOfTrade = c.locationOfTrade;
            this.PhoneNumber = c.PhoneNumber;
            this.AlternatePhone = c.AlternatePhone;
            this.Email = c.Email;
            this.Skype = c.Skype;
            this.OtherContact = c.OtherContact;

            this.Speciality = c.Speciality;
            this.lastPurchase = this.FirstMet;
            this.isInformed = true;
            this.Balance = 0;

            this.MoneySpent = 0;
            this.Level = 1;
        };

        $scope.createNewCust = function () {
            var cust = new Customer($scope.tempCustomer);
            dataService.postNewCust(cust, $scope.$parent);
            $modalInstance.close();
        };

        $scope.cancel = function () {
            $modalInstance.dismiss();
        };
    }
}
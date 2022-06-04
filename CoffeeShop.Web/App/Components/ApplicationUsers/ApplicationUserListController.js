(function (app) {
    app.controller('ApplicationUserListController', ApplicationUserListController);

    ApplicationUserListController.$inject = [
        '$scope',
        'ApiServices',
        'NotificationService',
        '$stateParams',
        '$state',
        '$http'
    ];

    function ApplicationUserListController($scope, ApiServices, NotificationService) {
        //setup Controller
        $scope.title = 'ApplicationUserListController';

        //Setup ApiServices

        $scope.applicationUsers = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyWord = '';
        //asign function to getApplicationUser
        $scope.getApplicationUsers = getApplicationUsers;
        $scope.showDeleteDialog = showDeleteDialog;
        $scope.deleteApplicationUser = deleteApplicationUser;

        $scope.showMultiDeleteDialog = showMultiDeleteDialog;
        $scope.deleteMultiApplicationUser = deleteMultiApplicationUser;

        function getApplicationUsers(page, pageSize) {
            page = page || 0;
            pageSize = pageSize || 20;

            var config = {
                params: {
                    page: page,
                    pageSize: pageSize,
                    filter: $scope.keyWord
                }
            }
            try {
                ApiServices.get('/api/ApplicationUser/GetListPaging', config, function (result) {
                    if (result.data.TotalCount == 0) {
                        NotificationService.displayWarning('No data to display');
                        var htmlResult = '<div style="width:100%;" class="alert alert-dark text-center" role="alert"><b>Không có dữ liệu để hiển thị</b></div> ';
                        $('#dataBody').html(htmlResult);
                    }

                    console.log(result.data)

                    $scope.listAppUsers = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages; //total pages that the query recevied
                    $scope.totalCount = result.data.TotalCount; //total row data from api result
                    $scope.itemPerPage = pageSize;
                },
                    function () {
                        console.log('LoadApplicationUser failed.');
                        NotificationService.displayError('LoadApplicationUser failed.');
                    });
            }
            catch (e) {
                console.log("Exception in get application groups function: ")
                    (console.error || console.log).call(console, e.stack || e);

                NotificationService.displayError('Something went wrong, please try again later');
            }
        }

        function showDeleteDialog(id) {
            $('#deleteId').val(id);
            $('#confirmDeleteModal').modal('show');
        }

        function deleteApplicationUser() {
            var id = $('#deleteId').val();
            var config = {
                params: {
                    id: id
                }
            }
            ApiServices.del('api/ApplicationUser/Delete', config, function () {
                NotificationService.displaySuccess('Xóa thành công');
                $('#confirmDeleteModal').modal('hide');
                getApplicationUsers();
            }, function () {
                $('#confirmDeleteModal').modal('hide');
                NotificationService.displayError('Xóa không thành công');
            })
        }

        function showMultiDeleteDialog() {
            var selectedItem = new Array();
            $('input:checkbox.checkBox').each(function () {
                if ($(this).prop('checked')) {
                    selectedItem.push($(this).val());
                }
            });

            console.log('delete selected count: ', selectedItem);

            if (selectedItem.length <= 0) {
                //$('m-content').html("Vui lòng chọn ít nhất một bản ghi để xóa!");
                //$('#delMultiBtn').hide();
                //$('#confirmMultiDeleteModal').modal('show');
            }
            else {
                $('#totalDeleteCount').html(selectedItem.length);
                $('#confirmMultiDeleteModal').modal('show');
            }
        }

        function deleteMultiApplicationUser() {
            var selectedIDs = [];
            $('input:checkbox.checkBox').each(function () {
                if ($(this).prop('checked')) {
                    selectedIDs.push($(this).val());
                }
            });

            //console.log('Console selectedIDs -> ', selectedIDs)

            var config = {
                params: {
                    ids: JSON.stringify(selectedIDs)
                }
            }

            //console.log('Param config: ', config)
            ApiServices.del('api/ApplicationUser/DeleteMulti', config, function (result) {
                NotificationService.displaySuccess('Xóa thành công');
                $('#confirmDeleteModal').modal('hide');
                getApplicationUsers();
            }, function (error) {
                NotificationService.displayError('Xóa không thành công');
                $('#confirmDeleteModal').modal('hide');
            })

            $('#confirmDeleteModal').modal('hide');
        }

        $scope.getApplicationUsers();
    }
})(angular.module('CoffeeShop.ApplicationUsers'));
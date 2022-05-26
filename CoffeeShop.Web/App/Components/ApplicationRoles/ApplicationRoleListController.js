(function (app) {
    app.controller('ApplicationRoleListController', ApplicationRoleListController);

    ApplicationRoleListController.$inject = [
        '$scope',
        'ApiServices',
        'NotificationService',
        '$stateParams',
        '$state',
        '$http'
    ];

    function ApplicationRoleListController($scope, ApiServices, NotificationService) {
        //setup Controller
        $scope.title = 'ApplicationRoleListController';

        //Setup ApiServices

        $scope.applicationRoles = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyWord = '';
        //asign function to getApplicationRole
        $scope.getApplicationRoles = getApplicationRoles;
        $scope.showDeleteDialog = showDeleteDialog;
        $scope.deleteApplicationRole = deleteApplicationRole;

        $scope.showMultiDeleteDialog = showMultiDeleteDialog;
        $scope.deleteMultiApplicationRole = deleteMultiApplicationRole;

        function getApplicationRoles(page, pageSize) {
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
                ApiServices.get('/api/ApplicationRole/GetListPaging', config, function (result) {
                    if (result.data.TotalCount == 0) {
                        NotificationService.displayWarning('No data to display');
                        var htmlResult = '<div style="width:100%;" class="alert alert-dark text-center" role="alert"><b>Không có dữ liệu để hiển thị</b></div> ';
                        $('#dataBody').html(htmlResult);
                    }

                    $scope.listAppRoles = result.data.Items;
                    console.log($scope.listAppRoles)
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages; //total pages that the query recevied
                    $scope.totalCount = result.data.TotalCount; //total row data from api result
                    $scope.itemPerPage = pageSize;
                },
                    function () {
                        console.log('LoadApplicationRole failed.');
                        NotificationService.displayError('LoadApplicationRole failed.');
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

        function deleteApplicationRole() {
            var id = $('#deleteId').val();
            var config = {
                params: {
                    id: id
                }
            }
            ApiServices.del('api/ApplicationRole/Delete', config, function () {
                NotificationService.displaySuccess('Xóa thành công');
                $('#confirmDeleteModal').modal('hide');
                getApplicationRoles();
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

        function deleteMultiApplicationRole() {
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
            ApiServices.del('api/ApplicationRole/DeleteMulti', config, function (result) {
                NotificationService.displaySuccess('Xóa thành công');
                $('#confirmDeleteModal').modal('hide');
                getApplicationRoles();
            }, function (error) {
                NotificationService.displayError('Xóa không thành công');
                $('#confirmDeleteModal').modal('hide');
            })

            $('#confirmDeleteModal').modal('hide');
        }

        $scope.getApplicationRoles();
    }
})(angular.module('CoffeeShop.ApplicationRoles'));
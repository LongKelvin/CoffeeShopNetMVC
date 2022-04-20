/// <reference path="../../shared/services/notificationservice.js" />
(function (app) {
    app.controller('SlideListController', SlideListController);

    SlideListController.$inject = [
        '$scope',
        'ApiServices',
        'NotificationService',
        '$stateParams',
        '$state',
        '$http'

    ];

    function SlideListController($scope, ApiServices, NotificationService) {
        //setup Controller
        $scope.title = 'SlideListController';

        //Setup ApiServices

        $scope.slides = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyWord = '';
        //asign function to get Slide
        $scope.getSlides = getSlides;
        $scope.showDeleteDialog = showDeleteDialog;
        $scope.deleteSlide = deleleSlide;

        $scope.showMultiDeleteDialog = showMultiDeleteDialog;
        $scope.deleteMultiSlide = deleteMultiSlide;

        function getSlides(page, pageSize) {
            page = page || 0;
            pageSize = pageSize || 20;

            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: pageSize
                }
            }
            try {
                ApiServices.get('/api/Slide/GetAll', config, function (result) {
                    if (result.data.TotalCount == 0) {
                        NotificationService.displayWarning('No data to display');
                    }

                    $scope.slides = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages; //total pages that the query recevied
                    $scope.totalCount = result.data.TotalCount; //total row data from api result
                    $scope.itemPerPage = pageSize;
                },
                    function () {
                        console.log('Load Slide failed.');
                        NotificationService.displayError('Load Slide failed.');
                    });
            }
            catch (e) {
                console.log("Exception in get slides function: ")
                    (console.error || console.log).call(console, e.stack || e);

                NotificationService.displayError('Something went wrong, please try again later');
            }
        }

        //function goToSlideAddView() {
        //    $state.go('SlideAdd')
        //}

        //excute when page loading done such as PageLoad

        function showDeleteDialog(id) {
            $('#deleteId').val(id);
            $('#pconfirmDeleteModal').modal('show');
        }

        function deleleSlide() {
            var id = $('#deleteId').val();
            var config = {
                params: {
                    id: id
                }
            }
            ApiServices.del('api/Slide/Delete', config, function () {
                NotificationService.displaySuccess('Xóa thành công');
                $('#pconfirmDeleteModal').modal('hide');
                getSlides();
            }, function () {
                $('#pconfirmDeleteModal').modal('hide');
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
                //$('#pconfirmMultiDeleteModal').modal('show');
            }
            else {
                $('#totalDeleteCount').html(selectedItem.length);
                $('#pconfirmMultiDeleteModal').modal('show');
            }
        }

        function deleteMultiSlide() {
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
            ApiServices.del('api/Slide/DeleteMultiItems', config, function () {
                NotificationService.displaySuccess('Xóa thành công');

                getSlides();
            }, function () {
                NotificationService.displayError('Xóa không thành công');
            })

            $('#pconfirmDeleteModal').modal('hide');
        }

        $scope.getSlides();
    }
})(angular.module('CoffeeShop.Slides'));
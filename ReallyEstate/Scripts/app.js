'use strict';

angular.module('reallyestate', ['ngRoute', 'ngResource'])

    .config(function($routeProvider) {

        var listingsConfig = {
            controller: 'ListingCtrl',
            templateUrl: 'Content/listings.html',
            resolve: {
                store: function(api) {
                    api.get();
                    return api;
                }
            }
        };

        var detailConfig = {
            controller: 'DetailCtrl',
            templateUrl: 'Content/detail.html'
        };

        $routeProvider
            .when('/', listingsConfig)
            .when('/:listing', detailConfig)
            .otherwise({
                redirectTo: '/'
            });

    })

    .controller('ListingCtrl', function ListingCtrl($scope, store) {

        var listings = $scope.listings = store.listings;

        $scope.newListing = '';

        $scope.addListing = function() {
            var newListing = {
                Address: $scope.newListing.address,
                Description: $scope.newListing.description,
                Price: $scope.newListing.price
            };

            if (!newListing.Address) {
                return;
            }

            $scope.isSaving = true;
            store.insert(newListing)
                .then(function success() {
                    $scope.newListing = '';
                })
            .finally(function() {
                    $scope.isSaving = false;
                });
        }

    })

    .controller('DetailCtrl', function DetailCtrl($scope, $location, $routeParams, api) {

        var listing = $scope.listing = api.get($routeParams.listing);

        $scope.deleteListing = function () {
            api.delete(listing);
            $location.path('/');
        }

        $scope.saveListing = function () {
            api.put(listing);
            $location.path('/');
        }

    })

    .factory('api', function($filter, $resource) {
        var store = {
            listings: [],

            api: $resource('/api/listings/:id', null,
            {
                update: { method: 'PUT' }
            }),

            delete: function(listing) {
                return store.api.delete({ Id: listing.Id });
            },

            get: function(listing) {
                if (listing) {
                    return $filter('filter')(store.listings, { Id: listing })[0];
                } else {
                    return store.api.query(function(res) {
                        angular.copy(res, store.listings);
                    });
                }
            },

            insert: function(listing) {
                return store.api.save(listing,
                    function(res) {
                        listing.Id = res.Id;
                        store.listings.push(listing);
                    })
                    .$promise;
            },

            put: function(listing) {
                return store.api.update({ Id: listing.Id }, listing)
                    .$promise;
            }
        };

        return store;
    });
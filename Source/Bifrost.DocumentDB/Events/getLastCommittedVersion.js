/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
function getLastCommittedVersion(eventSourceId) {
    var context = getContext();
    var collection = context.getCollection();
    var response = context.getResponse();

    collection.queryDocuments(collection.getSelfLink(),
        "SELECT * FROM Events WHERE EventSourceId = '"+eventSourceId+"'",
        {},
        function (error, documents, responseOptions) {
            var version = 0.0;

            if (typeof documents != "undefined" && documents != null) {
                documents.forEach(function (document) {
                    if (document.version > version) {
                        version = document.version;
                    }
                });
            }

            response.setBody(version);
        });
}
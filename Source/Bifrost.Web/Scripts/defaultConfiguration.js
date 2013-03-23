Bifrost.features.featureMapper.add("{feature}/{subFeature}", "/Features/{feature}/{subFeature}", false);
Bifrost.features.featureMapper.add("{feature}", "/Features/{feature}", true);

var defaultNamespaceMapper = Bifrost.StringMapper.create();
defaultNamespaceMapper.addMapping("**/", "**.");
Bifrost.namespaceMappers.default = defaultNamespaceMapper;

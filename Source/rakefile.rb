task :default do
  puts "All directories"
  Dir["*/"].map do |dir|
    puts "Restore packages for '#{dir}'"
    sh "mono --runtime='v4.0' Solutions/.nuget/NuGet.exe install #{dir}/packages.config -o 'Solutions/packages'" if File::exists?("#{dir}/packages.config")
  end
end
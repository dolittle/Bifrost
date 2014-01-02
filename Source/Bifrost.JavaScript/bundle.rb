@output = ""

File.readlines("Bifrost.js").each do |line|
  if line.include? "@depends "
    print "."
    filename = line[9,line.length].strip()
    # puts "Reading : '"+filename+"'"
    content = File.read(filename).strip()
    @output.concat(content)
    @output.concat("\n")
  end
end

file = File.open("Bifrost.debug.js", "w")
file.write(@output)
file.close unless file == nil

puts ""
puts "Done"
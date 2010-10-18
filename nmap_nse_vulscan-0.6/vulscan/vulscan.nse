description = [[
This is a module which enhances nmap to a vulnerability scanner. The
nmap option -sV enables version detection per service which is used to
determine potential flaws according to the identified product. The data
is looked up in an offline version of the osvdb. At osvdb.org an open-
source vulnerability data base is provided which serves information
regarding published security vulnerabilities.

Keep in mind that this kind of vulnerability scanning heavily relies on
the confidence of the version detection of nmap and the amount of
documented vulnerebilities. The existence of potential flaws is not
verified with additional scanning nor exploiting.

If you want to upgrade your database, go to the osvdb web site and
download the cvs export of the database. The exports are updated once a
day. Replace the txt files in your nmap nse vulscan script folder:

* object_products.txt      (name of all products)
* object_correlations.txt  (correlation of vendor/product/version)
* object_links.txt         (link between products and vulnerabilities)
* vulnerabilities.txt      (all documented vulnerabilities)
]]

--@output
-- PORT   STATE SERVICE REASON  VERSION
-- 25/tcp open  smtp    syn-ack Exim smtpd 4.69
-- | vulscan: [5330] Exim Configuration File Variable Overflow
-- | [5896] Exim sender_verify Function Remote Overflow
-- | [5897] Exim header_syntax Function Remote Overflow
-- | [5930] Exim Parenthesis File Name Filter Bypass
-- | [12726] Exim -be Command Line Option host_aton Function Local Overflow
-- | [12727] Exim SPA Authentication spa_base64_to_bits Function Remote Overflow
-- |_[12946] Exim -bh Command Line Option dns_build_reverse Function Local Overflow

--@changelog
-- v0.6 | 05/22/2010 | Marc Ruef | Added interactive mode for guided testing
-- v0.5 | 05/21/2010 | Marc Ruef | Seperate functions for search engine
-- v0.4 | 05/20/2010 | Marc Ruef | Tweaked analysis modules
-- v0.3 | 05/19/2010 | Marc Ruef | Fuzzy search for product names included
-- v0.2 | 05/18/2010 | Marc Ruef | Uniqueness of found vulnerabilities
-- v0.1 | 05/17/2010 | Marc Ruef | First alpha running basic identification

--@bugs
-- Fuzzy search is considering object_products only which causes false-negatives
-- Fuzzy search is not matching product names which contain vendor names
-- Fuzzy search is sometimes catching wrong products
-- Split function does not take quotes of cve data into account

--@todos
-- Enhance fuzzy search to match vendors (e.g. "Microsoft IIS httpd")
-- Take port.version.version / object_versions into account
-- Create product lookup table to match nmap<->osvdb
-- Enhance nmap/osvdb to be CPE compliant (http://cpe.mitre.org)
-- Display of identification confidence (e.g. +full_match, -partial_match)
-- Add dynamic report output templates like within the ATK project
-- Add support for user arguments to change scan behavior
-- Add auto-update feature for osvdb database (download & untar)

--@thanks
-- I would like to thank a number of people which supported my in
-- developing this script: Stefan Friedli, Simon Zumstein, David
-- Fifield and Doggy Dog.

author = "Marc Ruef, marc.ruef-at-computec.ch, http://www.computec.ch/mruef/"
license = "Same as Nmap--See http://nmap.org/book/man-legal.html"
categories = {"default", "safe"}

require("stdnse")

portrule = function(host, port)
	if port.version.product ~= nil and port.version.product ~= "" then
		return true
	end
end

action = function(host, port)
	stdnse.print_debug(1, "vulscan: Found service " .. port.version.product)
	local mode = "titles"
	local result = ""

	if nmap.registry.args.vulscancorrelation == "1" then
		stdnse.print_debug(1, "vulscan: Starting search mode correlations ...")
		local products_matches = find_products_name(port.version.product)

		if #products_matches > 0 then
			local correlations_matches = find_correlations(products_matches)

			if #correlations_matches > 0 then
			local links_matches = find_links(correlations_matches)

				if #links_matches > 0 then
					local vulnerabilities_matches = find_vulnerabilities(links_matches, "id")

					if #vulnerabilities_matches > 0 then
						stdnse.print_debug(1, "vulscan: " .. #vulnerabilities_matches .. " vulnerabilities found")
						return prepare_result(vulnerabilities_matches)
					end
				end
			end
		end
	else
		stdnse.print_debug(1, "vulscan: Starting search mode title ...")
		local vulnerabilities_matches = find_vulnerabilities(prepare_fuzzy_search(port.version.product), "title")

		if #vulnerabilities_matches > 0 then
			stdnse.print_debug(1, "vulscan: " .. #vulnerabilities_matches .. " vulnerabilities found")
			return prepare_result(vulnerabilities_matches)
		end
	end
end

function find_products_name(product, database)
	local object_products = read_from_file("scripts/vulscan/object_products.txt")
	local fuzzy_search_string = prepare_fuzzy_search(product)
	local products_matches = {}
	local products_name
	local products_found

	for i=1, #fuzzy_search_string, 1 do
		-- Find the product in the object_products table
		for j=1, #object_products, 1 do
			products_name = extract_value_from_table(object_products[j], 2)

			if type(products_name) == "string" then
				products_name = cut_quotes(products_name)
				products_found = string.find(products_name, escape(fuzzy_search_string[i]))

				if type(products_found) == "number" then
					stdnse.print_debug(1, "vulscan: Products id " .. extract_value_from_table(object_products[j], 2) .. " (object_products)")
					products_matches[#products_matches+1] = extract_value_from_table(object_products[j], 1)
				end
			end
		end

		-- We use the best match(es) only
		if #products_matches > 0 then
			stdnse.print_debug(1, "vulscan: Best fuzzy match was " .. fuzzy_search_string[i])
			break
		end
	end

	return products_matches
end

function find_correlations(products_matches)
	local object_correlations = read_from_file("scripts/vulscan/object_correlations.txt")
	local correlations_matches = {}
	local correlations_id
	local object_correlations_toadd

	for i=1, #object_correlations, 1 do
		for j=1, #products_matches, 1 do
			correlations_id = extract_value_from_table(object_correlations[i], 3)

			if type(correlations_id) == "string" then
				if correlations_id == products_matches[j] then
					object_correlations_toadd = extract_value_from_table(object_correlations[i], 1)

					if in_array(correlations_matches, object_correlations_toadd) == nil then
						stdnse.print_debug(1, "vulscan: Correlation id " .. object_correlations_toadd .. " (object_correlations) from products id " .. products_matches[j])
						correlations_matches[#correlations_matches+1] = object_correlations_toadd
					end
				end
			end

		end
	end

	return correlations_matches
end

function find_links(correlations_matches)
	local object_links = read_from_file("scripts/vulscan/object_links.txt")
	local links_matches = {}
	local links_id
	local object_links_toadd
	local object_links_affect

	for i=1, #object_links, 1 do
		for j=1, #correlations_matches, 1 do
			links_id = extract_value_from_table(object_links[i], 3)

			if type(links_id) == "string" then
				if links_id == correlations_matches[j] then
					object_links_toadd = extract_value_from_table(object_links[i], 2)
					object_links_affect = extract_value_from_table(object_links[i], 4)

					-- Verify that the object is affected; values defined in object_affect_types.txt
					if object_links_affect == "1" or object_links_affect == "3" then
						if in_array(links_matches, object_links_toadd) == nil then
							stdnse.print_debug(1, "vulscan: Vulnerability id " .. object_links_toadd .. " (object_links) from correlations id " .. links_id)
							links_matches[#links_matches+1] = object_links_toadd
						end
					end
				end
			end
		end
	end

	return links_matches
end

function find_vulnerabilities(pattern, mode)
	local vulnerabilities = read_from_file("scripts/vulscan/vulnerabilities.txt")
	local vulnerabilities_matches = {}
	local vulnerability_id
	local vulnerabilities_title
	local vulnerabilities_toadd
	local vulnerabilities_found

	for i=1, #pattern, 1 do
		stdnse.print_debug(1, "vulscan: Fuzzy search for " .. pattern[i] .. " ...")

		for j=1, #vulnerabilities, 1 do
			vulnerability_id = extract_value_from_table(vulnerabilities[j], 1)

			if type(vulnerability_id) == "string" then
				if (mode == "id" and vulnerability_id == pattern[i]) then
					vulnerabilities_id = extract_value_from_table(vulnerabilities[j], 2)
					vulnerabilities_title = extract_value_from_table(vulnerabilities[j], 3)

					if in_array(vulnerabilities_matches, vulnerabilities_id) == nil then
						stdnse.print_debug(1, "vulscan: Vulnerabilities id " .. vulnerability_id .. " (vulnerabilities)")
						vulnerabilities_matches[#vulnerabilities_matches+1] = {
							id = vulnerabilities_id,
							title = cut_quotes(vulnerabilities_title)
						}
					end
				elseif mode == "title" then
					vulnerabilities_id = extract_value_from_table(vulnerabilities[j], 2)
					vulnerabilities_title = extract_value_from_table(vulnerabilities[j], 3)

					if type(vulnerabilities_title) == "string" then
						vulnerabilities_found = string.find(vulnerabilities_title, "^" .. escape(pattern[i]))

						if type(vulnerabilities_found) == "number" then
							stdnse.print_debug(1, "vulscan: Vulnerabilities id " .. vulnerability_id .. " (vulnerabilities)")
							vulnerabilities_matches[#vulnerabilities_matches+1] = {
								id = vulnerabilities_id,
								title = cut_quotes(vulnerabilities_title)
							}
						end
					end
				end
			end

		end

		-- We use the best match(es) only
		if mode == "title" and #vulnerabilities_matches > 0 then
			break
		end
	end

	return vulnerabilities_matches
end

function prepare_fuzzy_search(product)
	local products_wordsearch
	local fuzzy_search = {}

	if nmap.registry.args.vulscaninteractive == "1" then
		print("The scan has determined the following product:")
		print(product)
		print("Press Enter to accept. Define new string to override.")
		local product_override = io.stdin:read'*l'

		if string.len(product_override) ~= 0 then
			product = product_override
		end
	end

	local products_words = stdnse.strsplit(" ", product)
	for i=1, 2, 1 do
		for j=#products_words, 1+(i-1), -1 do
			products_wordsearch = ""

			-- Generate a best match string for the product name
			for k=i, j, 1 do
				if products_wordsearch == "" then
					products_wordsearch = products_words[k]
				else
					products_wordsearch = products_wordsearch .. " " .. products_words[k]
				end
			end

			fuzzy_search[#fuzzy_search+1] = products_wordsearch
		end
	end

	return fuzzy_search
end

function prepare_result(vulnerabilities_matches)
	local result = ""

	for i=1, #vulnerabilities_matches, 1 do
		result = result .. "[" .. vulnerabilities_matches[i].id .. "] " ..
			vulnerabilities_matches[i].title .. "\n"
	end

	return result
end

function extract_value_from_table(tableline, column)
	local values = stdnse.strsplit(",", tableline)

	if type(values[column]) == "string" then
		return values[column]
	end
end

function filter_value_from_record(tableline, searchcolumn, searchstring, resultcolumn)
	local searchcolumn_data = extract_value_from_table(tableline, searchcolumn)

	if type(searchcolumn_data) == "string" then
		if searchcolumn_data == searchstring then
			local resultcolumn_data = extract_value_from_table(tableline, resultcolumn)

			if type(resultcolumn_data) == "string" then
				return resultcolumn_data
			end
		end
	end
end

function read_from_file(file)
	local filepath = nmap.fetchfile(file)

	if not filepath then
		stdnse.print_debug(1, "vulscan: File %s not found", file)
	end

	local f, err, _ = io.open(filepath, "r")
	if not f then
		stdnse.print_debug(1, "vulscan: Failed to open file %s", file)
	end

	local line, ret = nil, {}
	while true do
		line = f:read()
		if not line then break end
		ret[#ret+1] = line
	end

	f:close()

	return ret
end

function in_array(array, find)
	for i=1, #array, 1 do
		if array[i] == find then
			return i
		end
	end
end

function cut_quotes(str)
	return string.gsub(str, "^\"(.-)\"$", "%1")
end

function escape(str)
	local escape_characters = { "%(", "%)", "%.", "%%", "%+", "%-", "%*", "%?", "%[", "%]", "%^", "%$" }

	for i=1, #escape_characters, 1 do
		str = string.gsub(str, escape_characters[i], "%" .. escape_characters[i])
	end

	return str
end

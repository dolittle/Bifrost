<?xml version="1.0" encoding="utf-8"?>

<!-- 
  Converts the MSpec (http://github.com/machine/machine.specifications) xml 
  output to JUnit output format.
  
  The aim of this transformation is to integrate MSpec tests to Hudson CI. 
  Hudson understands test results presented in JUnit format.
  
  Limitations: MSpec xml output currently doesn't contain detailed information 
  concerning tests failures and tests durations. So for such information JUnit 
  report is filled with fake data.
-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes"/>

  <xsl:variable name="specs" select="//specification"/>
  
  <xsl:template match="MSpec">
    <testsuite 
      name="Specifications" 
      tests="{count($specs)}"
      errors="0" 
      failures="{count($specs[@status='failed'])}"
      skipped="{count($specs[@status='not-implemented' or @status='ignored'])}">
      <xsl:apply-templates select="//specification"/>
    </testsuite>
  </xsl:template>

  <xsl:template match="specification">
    <xsl:variable name="classname">
      <xsl:value-of select="ancestor::assembly/@name"/>.<xsl:value-of select="ancestor::concern/@name"/>.<xsl:value-of select="ancestor::context/@name"/>
    </xsl:variable>
    
    <testcase classname="{$classname}" name="{./@name}">
      <xsl:if test="@status = 'failed'">
        <failure message="Specification failed">See html report for details</failure>
      </xsl:if>
      <xsl:if test="@status = 'not-implemented' or @status = 'ignored'">
        <skipped message="Specification skipped">
        </skipped>
      </xsl:if>
    </testcase>
  </xsl:template>
</xsl:stylesheet>

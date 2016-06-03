<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

  <xsl:template match="@zip[string-length(.)=5]">
    <xsl:attribute name="zip">
      <xsl:value-of select="concat(., '-1234')"/>
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="@state[.='ME']">
    <xsl:copy/>
    <xsl:attribute name="local">
      <xsl:value-of select="'true'"/>
    </xsl:attribute>
  </xsl:template>
</xsl:stylesheet>

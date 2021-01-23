using System;
using Moq;
using Shouldly;
using TableLogger;
using Xunit;

namespace UnitTests
{
    public class TableLoggerTests
    {
        [Fact]
        public void AddRowData_ShouldThrowException_WhenNoDataProvided()
        {
            var sut = new global::TableLogger.TableLogger();
         
            Should.Throw<ArgumentException>(() => sut.AddRow());
        }

        [Fact]
        public void Columns_ShouldReturnOneColumn_WhenOneColumnIsSpecifiedInConstructor()
        {
            var sut = new global::TableLogger.TableLogger("Id");
         
            sut.Columns.ShouldNotBeEmpty();
        }

        [Fact]
        public void Rows_ShouldReturnOneRow_WhenOneRowIsAdded()
        {
            var sut = new global::TableLogger.TableLogger("Id");

            sut.AddRow("1");
         
            sut.Rows.ShouldNotBeEmpty();
        }

        [Fact]
        public void WriteTable_ShouldCallWriteLineOnLogProvider_WhenThereIsARowDefined()
        {
            var logProviderMock = new Mock<ILogProvider>();
            var sut = new global::TableLogger.TableLogger("Id");
            sut.AddLogProvider(logProviderMock.Object);
            sut.AddRow("1");

            sut.WriteTable();
         
            logProviderMock.Verify(x=>x.WriteLine(It.IsAny<string>()), Times.Once);
        }

    }
}